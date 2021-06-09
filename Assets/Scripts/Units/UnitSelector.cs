using System;
using CarSumo.Data;
using CarSumo.Extensions;
using UnityEngine;
using CarSumo.Input;
using CarSumo.Teams;
using CarSumo.VFX;
using Cinemachine.Utility;
using Sirenix.OdinInspector;

namespace CarSumo.Units
{
    public class UnitSelector : SerializedMonoBehaviour, ITeamChangeSender
    {
        public event Action ChangeSent;

        [SerializeField] private UnitSelectorDataProvider _dataProvider;

        [Header("Components")]
        [SerializeField] private ISwipePanel _panel;
        [SerializeField] private ITeamChangeHandler _handler;
        [SerializeField] private Camera _camera;

        [Header("FX")] 
        [SerializeField] private EnablersEmitter _targetCircle;
        [SerializeField] private Text3DEmitter _pushForceTextEmitter;
        
        private Unit _selectedUnit;

        private bool _isMoveCompleted = true;
        private bool _canceled;

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
            var ray = _camera.ScreenPointToRay(data.EndPosition);

            if (Physics.Raycast(ray, out var hit) == false)
                return;

            if (hit.collider.TryGetComponent<Unit>(out var unit) == false)
                return;

            if (unit.Team != _handler.Team)
                return;

            if (_isMoveCompleted == false)
                return;

            _canceled = false;

            _selectedUnit = unit;

            _targetCircle.Emit(_selectedUnit.transform);
            _pushForceTextEmitter.Emit(_selectedUnit.transform);
        }

        private void OnPanelSwiping(SwipeData data)
        {
            if (_selectedUnit is null)
                return;

            var percentage = _dataProvider.CalculatePercentage(data.Distance);
            _canceled = percentage <= _dataProvider.CancelDistancePercent;

            _pushForceTextEmitter.SetText($"{(int)percentage}%");
            _pushForceTextEmitter.SetForwardVector(_camera.transform.forward);

            if (data.Distance <= _dataProvider.MinSelectDistance)
                return;
            
            var direction = new Vector3
            {
                x = data.Direction.x,
                z = data.Direction.y
            };
            
            var transformedDirection = _camera.GetRelativeDirection(direction)
                                                    .ProjectOntoPlane(Vector3.up)
                                                    .normalized;

            _selectedUnit.Rotate(transformedDirection);
        }

        private void OnPanelSwipeReleased(SwipeData data)
        {
            if (_selectedUnit is null)
                return;

            _isMoveCompleted = false;

            _targetCircle.Stop();
            _pushForceTextEmitter.Stop();

            if (_canceled)
            {
                _isMoveCompleted = true;
                _selectedUnit = null;
                return;
            }

            _selectedUnit.ChangeSent += InvokeTeamChangeRequest;
            var multiplier = _dataProvider.CalculateAccelerationMultiplier(data.Distance);
            _selectedUnit.Push(multiplier);
        }

        private void InvokeTeamChangeRequest()
        {
            ChangeSent?.Invoke();
            _selectedUnit.ChangeSent -= InvokeTeamChangeRequest;
            _selectedUnit = null;

            _isMoveCompleted = true;
        }
    }
}