using UnityEngine;
using Sirenix.OdinInspector;
using CarSumo.Teams;
using CarSumo.Input;
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
        private SelectorSpeedometer _speedometer;

        private Vehicle _selectedVehicle;
        private bool _isMoveCompleted = true;

        private void Awake()
        {
            LastValidVehicles = new VehicleCollection();
            _vehiclePicker = new VehiclePicker(_camera, _changeHandler);
            _speedometer = new SelectorSpeedometer(_data);
        }

        private void OnEnable()
        {
            _panel.Begun += OnPanelSwipeBegun;
        }

        private void OnDisable()
        {
            _panel.Begun -= OnPanelSwipeBegun;
        }

        private void OnPanelSwipeBegun(SwipeData swipeData)
        {
            if (_isMoveCompleted)
                return;

            if (_vehiclePicker.TryPickVehicle(swipeData, out _selectedVehicle) == false)
                return;

            _selectedVehicle.Engine.TurnOn(_speedometer);

        }
    }
}
