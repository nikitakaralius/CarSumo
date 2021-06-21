using System;
using CarSumo.Data;
using CarSumo.Extensions;
using CarSumo.Factory;
using UnityEngine;
using CarSumo.Input;
using CarSumo.Teams;
using CarSumo.Vehicles.Stats;
using CarSumo.VFX;
using Cinemachine.Utility;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

namespace CarSumo.Vehicles
{
    public class VehicleSelector : SerializedMonoBehaviour, ITeamChangeSender
    {
        public event Action TeamChangePerformed;

        public Vehicle[] LastActingVehicles { get; private set; }

        [SerializeField] private UnitSelectorDataProvider _dataProvider;

        [Header("Components")]
        [SerializeField] private ISwipePanel _panel;
        [SerializeField] private ITeamChangeHandler _teamChangeHandler;
        [SerializeField] private Camera _camera;

        [Header("FX")]
        [SerializeField] private Text3DEmitter _pushForceTextEmitter;

        [InfoBox("Unit Selected Emitters require to copy particles from other FX fields")]
        [SerializeField] private EmitterScriptableObject[] _unitSelectedEmitters;
        
        private Vehicle _selectedVehicle;

        private bool _isMoveCompleted = true;
        private bool _pushCanceled;

        private void Awake()
        {
            LastActingVehicles = new Vehicle[2];
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

        private void OnPanelSwipeBegun(SwipeData data)
        {
            if (_isMoveCompleted == false)
                return;

            if (_camera.TryGetComponentWithRaycast(data.EndPosition, out Vehicle vehicle) == false)
                return;

            if (CanPickVehicle(vehicle) == false)
                return;

            _selectedVehicle = vehicle;
            _selectedVehicle.Pick();
            _unitSelectedEmitters.ForEach(emitter => emitter.Emit(_selectedVehicle.transform));
            AddVehicleToList(_selectedVehicle);
        }

        private void OnPanelSwiping(SwipeData data)
        {
            if (_selectedVehicle is null)
                return;

            if (_isMoveCompleted == false)
                return;

            var pushPercentage = _dataProvider.CalculatePercentage(data.Distance);
            _pushCanceled = pushPercentage <= _dataProvider.CancelDistancePercent;

            _pushForceTextEmitter.SetText((int)pushPercentage);
            _pushForceTextEmitter.SetForwardVector(_camera.transform.forward);

            if (data.Distance <= _dataProvider.MinSelectDistance)
                return;

            var transformedDirection = GetTransformedDirection(data.Direction);
            _selectedVehicle.RotateByForwardVector(transformedDirection);
            _selectedVehicle.PassPowerPercentage(pushPercentage);
        }

        private void OnPanelSwipeReleased(SwipeData data)
        {
            if (_selectedVehicle is null)
                return;

            if (_isMoveCompleted == false)
                return;

            _isMoveCompleted = false;
            _unitSelectedEmitters.ForEach(emitter => emitter.Stop());

            if (_pushCanceled)
            {
                _selectedVehicle.Unpick();
                CompleteMove();
                return;
            }

            _selectedVehicle.TeamChangePerformed += InvokeTeamChangeRequest;
            _selectedVehicle.Upgrading += OnVehicleUpgrade;
            var multiplier = _dataProvider.CalculateAccelerationMultiplier(data.Distance);
            _selectedVehicle.PushForward(multiplier);
        }

        private void InvokeTeamChangeRequest()
        {
            if (_selectedVehicle is null)
                return;

            TeamChangePerformed?.Invoke();
            _selectedVehicle.Upgrading -= OnVehicleUpgrade;
            _selectedVehicle.TeamChangePerformed -= InvokeTeamChangeRequest;
            CompleteMove();
        }

        private void OnVehicleUpgrade()
        {
            if (_selectedVehicle is null)
                return;

            _selectedVehicle.TeamChangePerformed -= InvokeTeamChangeRequest;
            _selectedVehicle.Upgrading -= OnVehicleUpgrade;
            CompleteMove();
        }

        private void CompleteMove()
        {
            _selectedVehicle = null;
            _isMoveCompleted = true;
        }

        private bool CanPickVehicle(IVehicleStatsProvider vehicle)
        {
            return vehicle.GetStats().Team == _teamChangeHandler.Team;
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

        private void AddVehicleToList(Vehicle vehicle)
        {
            var index = (int)vehicle.GetStats().Team;
            LastActingVehicles[index] = vehicle;
        }
    }
}