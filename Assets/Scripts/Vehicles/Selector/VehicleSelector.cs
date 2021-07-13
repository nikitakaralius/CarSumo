using CarSumo.Coroutines;
using CarSumo.Infrastructure.Services.TeamChangeService;
using CarSumo.Infrastructure.Services.TimerService;
using CarSumo.Input;
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
        [SerializeField] private Camera _camera;

        private VehiclePicker _vehiclePicker;
        private VehicleBoostConfiguration _boost;
        private SelectorMoveHandler _moveHandler;

        private SelectorSpeedometer _speedometer;

        private Vehicle _selectedVehicle;
        
        private ISwipeScreen _screen;
        private ITeamChangeService _teamChangeService;
        private ITimerService _timerService;

        [Inject]
        private void Construct(ISwipeScreen swipeScreen, ITeamChangeService teamChangeService, ITimerService timerService)
        {
            _screen = swipeScreen;
            _teamChangeService = teamChangeService;
            _timerService = timerService;
        }

        public VehicleCollection LastValidVehicles { get; private set; }

        private void Awake()
        {
            var executor = new CoroutineExecutor(this);

            LastValidVehicles = new VehicleCollection();
            _speedometer = new SelectorSpeedometer(_data);

            _vehiclePicker = new VehiclePicker(_camera, LastValidVehicles, _speedometer, _teamChangeService);
            _boost = new VehicleBoostConfiguration(_camera, _speedometer);
            _moveHandler = new SelectorMoveHandler(_teamChangeService, _speedometer, _data, executor, _timerService);
        }

        private void OnEnable()
        {
            _screen.Begun += OnScreenSwipeBegun;
            _screen.Swiping += OnScreenSwiping;
            _screen.Released += OnScreenSwipeReleased;

            _teamChangeService.TeamChanged += _boost.TurnOffActiveVehicle;
        }

        private void OnDisable()
        {
            _screen.Begun -= OnScreenSwipeBegun;
            _screen.Swiping -= OnScreenSwiping;
            _screen.Released -= OnScreenSwipeReleased;
            
            _teamChangeService.TeamChanged -= _boost.TurnOffActiveVehicle;
        }

        private void OnScreenSwipeBegun(SwipeData swipeData)
        {
            if (_moveHandler.CanPerformMove() == false)
                return;

            _selectedVehicle = _vehiclePicker.GetVehicleBySwipe(swipeData);
        }

        private void OnScreenSwiping(SwipeData swipeData)
        {
            if (_moveHandler.CanPerformMove() == false)
                return;

            if (_vehiclePicker.IsValid(_selectedVehicle) == false)
                return;

            if (swipeData.Distance <= _data.MinSelectDistance)
                return;

            _boost.ConfigureBoost(_selectedVehicle, swipeData);
        }

        private void OnScreenSwipeReleased(SwipeData swipeData)
        {
            if (_moveHandler.CanPerformMove() == false)
                return;

            if (_vehiclePicker.IsValid(_selectedVehicle) == false)
                return;

            _moveHandler.HandleVehiclePush(_selectedVehicle, swipeData);
        }
    }
}
