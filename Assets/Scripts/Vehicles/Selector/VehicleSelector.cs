using UnityEngine;
using CarSumo.Teams;
using CarSumo.Input;
using Sirenix.OdinInspector;
using CarSumo.Vehicles.Speedometers;

namespace CarSumo.Vehicles.Selector
{
    public class VehicleSelector : SerializedMonoBehaviour
    {
        public VehicleCollection LastValidVehicles { get; private set; }

        [SerializeField] private VehicleSelectorData _data;

        [Header("Components")]
        [SerializeField] private ISwipePanel _panel;
        [SerializeField] private ITeamChangeHandler _changeHandler;
        [SerializeField] private Camera _camera;

        private VehiclePicker _vehiclePicker;
        private VehicleBoostConfiguration _boost;
        private SelectorSpeedometer _speedometer;

        private Vehicle _selectedVehicle;
        private bool _isMoveCompleted = true;

        private void Awake()
        {
            LastValidVehicles = new VehicleCollection();
            _speedometer = new SelectorSpeedometer(_data);

            _vehiclePicker = new VehiclePicker(_camera, LastValidVehicles, _speedometer, _changeHandler);
            _boost = new VehicleBoostConfiguration(_camera, _speedometer);
        }

        private void OnEnable()
        {
            _panel.Begun += OnPanelSwipeBegun;
            _panel.Swiping += OnPanelSwiping;
        }

        private void OnDisable()
        {
            _panel.Begun -= OnPanelSwipeBegun;
            _panel.Swiping -= OnPanelSwiping;
        }

        private void OnPanelSwipeBegun(SwipeData swipeData)
        {
            if (_isMoveCompleted == false)
                return;

            _selectedVehicle = _vehiclePicker.GetVehicleBySwipe(swipeData);
        }

        private void OnPanelSwiping(SwipeData swipeData)
        {
            if (_isMoveCompleted == false)
                return;

            if (_vehiclePicker.IsValid(_selectedVehicle) == false)
                return;

            if (swipeData.Distance <= _data.MinSelectDistance)
                return;

            _boost.ConfigureBoost(_selectedVehicle, swipeData);
        }

        private void OnPanelSwipeReleased(SwipeData swipeData)
        {
            if (_isMoveCompleted == false)
                return;

            if (_vehiclePicker.IsValid(_selectedVehicle) == false)
                return;

            _isMoveCompleted = false;
        }
    }

    public class SelectorMoveHandler
    {
        private readonly ITeamChangeHandler _changeHandler;
        private readonly IVehicleSpeedometer _speedometer;
        private readonly VehicleSelectorData _data;
        private readonly CoroutineExecutor _executor;

        private bool _isMovePerforming = false;

        public SelectorMoveHandler(ITeamChangeHandler changeHandler,
                                IVehicleSpeedometer speedometer,
                                VehicleSelectorData data,
                                CoroutineExecutor executor)
        {
            _changeHandler = changeHandler;
            _speedometer = speedometer;
            _data = data;
            _executor = executor;
        }

        public void HadnleVehiclePush(Vehicle vehicle, SwipeData swipeData)
        {
            if (_isMovePerforming)
                return;

            if (_speedometer.PowerPercentage <= _data.CancelDistancePercent)
                return;

            _isMovePerforming = true;
        }
    }
}
