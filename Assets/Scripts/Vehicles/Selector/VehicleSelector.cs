using CarSumo.Input;
using CarSumo.Teams;
using CarSumo.Vehicles.Speedometers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarSumo.Vehicles.Selector
{
    public class VehicleSelector : SerializedMonoBehaviour
    {
        [SerializeField] private VehicleSelectorData _data;

        [Header("Components")]
        [SerializeField] private ISwipePanel _panel;
        [SerializeField] private ITeamChangeHandler _changeHandler;
        [SerializeField] private Camera _camera;

        private VehiclePicker _vehiclePicker;
        private VehicleBoostConfiguration _boost;
        private SelectorMoveHandler _moveHandler;

        private SelectorSpeedometer _speedometer;

        private Vehicle _selectedVehicle;

        public VehicleCollection LastValidVehicles { get; private set; }

        private void Awake()
        {
            var executor = new CoroutineExecutor(this);

            LastValidVehicles = new VehicleCollection();
            _speedometer = new SelectorSpeedometer(_data);

            _vehiclePicker = new VehiclePicker(_camera, LastValidVehicles, _speedometer, _changeHandler);
            _boost = new VehicleBoostConfiguration(_camera, _speedometer);
            _moveHandler = new SelectorMoveHandler(_changeHandler, _speedometer, _data, executor);
        }

        private void OnEnable()
        {
            _panel.Begun += OnPanelSwipeBegun;
            _panel.Swiping += OnPanelSwiping;
            _panel.Released += OnPanelSwipeReleased;
        }

        private void OnDisable()
        {
            _panel.Begun -= OnPanelSwipeBegun;
            _panel.Swiping -= OnPanelSwiping;
            _panel.Released -= OnPanelSwipeReleased;
        }

        private void OnPanelSwipeBegun(SwipeData swipeData)
        {
            if (_moveHandler.CanPeformMove() == false)
                return;

            _selectedVehicle = _vehiclePicker.GetVehicleBySwipe(swipeData);
        }

        private void OnPanelSwiping(SwipeData swipeData)
        {
            if (_moveHandler.CanPeformMove() == false)
                return;

            if (_vehiclePicker.IsValid(_selectedVehicle) == false)
                return;

            if (swipeData.Distance <= _data.MinSelectDistance)
                return;

            _boost.ConfigureBoost(_selectedVehicle, swipeData);
        }

        private void OnPanelSwipeReleased(SwipeData swipeData)
        {
            if (_moveHandler.CanPeformMove() == false)
                return;

            if (_vehiclePicker.IsValid(_selectedVehicle) == false)
                return;

            _moveHandler.HadnleVehiclePush(_selectedVehicle, swipeData);
        }
    }
}
