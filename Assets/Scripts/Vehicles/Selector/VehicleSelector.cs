using CarSumo.Coroutines;
using CarSumo.Input;
using CarSumo.Teams;
using CarSumo.Vehicles.Speedometers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CarSumo.Vehicles.Selector
{
    public class VehicleSelector : SerializedMonoBehaviour
    {
        [SerializeField] private VehicleSelectorData _data;

        [Header("Components")]
        [SerializeField] private ITeamChangeHandler _changeHandler;
        [SerializeField] private Camera _camera;

        private VehiclePicker _vehiclePicker;
        private VehicleBoostConfiguration _boost;
        private SelectorMoveHandler _moveHandler;

        private SelectorSpeedometer _speedometer;

        private Vehicle _selectedVehicle;
        
        private ISwipeScreen _screen;

        [Inject]
        private void Construct(ISwipeScreen swipeScreen)
        {
            _screen = swipeScreen;
        }

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
            _screen.Begun += OnScreenSwipeBegun;
            _screen.Swiping += OnScreenSwiping;
            _screen.Released += OnScreenSwipeReleased;
        }

        private void OnDisable()
        {
            _screen.Begun -= OnScreenSwipeBegun;
            _screen.Swiping -= OnScreenSwiping;
            _screen.Released -= OnScreenSwipeReleased;
        }

        private void OnScreenSwipeBegun(SwipeData swipeData)
        {
            if (_moveHandler.CanPeformMove() == false)
                return;

            _selectedVehicle = _vehiclePicker.GetVehicleBySwipe(swipeData);
        }

        private void OnScreenSwiping(SwipeData swipeData)
        {
            if (_moveHandler.CanPeformMove() == false)
                return;

            if (_vehiclePicker.IsValid(_selectedVehicle) == false)
                return;

            if (swipeData.Distance <= _data.MinSelectDistance)
                return;

            _boost.ConfigureBoost(_selectedVehicle, swipeData);
        }

        private void OnScreenSwipeReleased(SwipeData swipeData)
        {
            if (_moveHandler.CanPeformMove() == false)
                return;

            if (_vehiclePicker.IsValid(_selectedVehicle) == false)
                return;

            _moveHandler.HadnleVehiclePush(_selectedVehicle, swipeData);
        }
    }
}
