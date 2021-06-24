using UnityEngine;
using Sirenix.OdinInspector;
using CarSumo.Teams;
using CarSumo.Input;
using CarSumo.Vehicles.Speedometers;

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
        private SelectorSpeedometer _spedometer;

        private Vehicle _selectedVehicle;
        private bool _isMoveCompleted = true;

        private void Awake()
        {
            _vehiclePicker = new VehiclePicker(_camera, _changeHandler);
            _spedometer = new SelectorSpeedometer(_data);
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

        }
    }
}
