using UnityEngine;
using CarSumo.Teams;
using CarSumo.Input;
using Sirenix.OdinInspector;
using CarSumo.Vehicles.Speedometers;
using CarSumo.Extensions;
using Cinemachine.Utility;

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
            _speedometer = new SelectorSpeedometer(_data);
            _vehiclePicker = new VehiclePicker(_camera, LastValidVehicles, _speedometer, _changeHandler);
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

            if (_vehiclePicker.IsValid() == false)
                return;

            if (swipeData.Distance <= _data.MinSelectDistance)
                return;

            var transformedDirection = GetTransformedDirection(swipeData.Direction);
            _selectedVehicle.Rotation.RotateBy(transformedDirection);
            _speedometer.CalculatePowerBySwipeData(swipeData);
        }

        private Vector3 GetTransformedDirection(Vector2 swipeDirection)
        {
            var direction = new Vector3
            {
                x = swipeDirection.x,
                z = swipeDirection.y
            };

            var transformedDirection = _camera.GetRelativeDirection(direction)
                                                    .ProjectOntoPlane(Vector3.up)
                                                    .normalized;

            return transformedDirection;
        }
    }
}
