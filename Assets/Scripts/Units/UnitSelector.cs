using System;
using System.Globalization;
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

            if (!_isMoveCompleted)
                return;

            _selectedUnit = unit;
            _selectedUnit.ChangeSent += InvokeTeamChangeRequest;

            _targetCircle.Emit(_selectedUnit.transform);
            _pushForceTextEmitter.Emit(_selectedUnit.transform);
        }

        private void OnPanelSwiping(SwipeData data)
        {
            if (_selectedUnit is null)
                return;

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

            _pushForceTextEmitter.SetText($"{(int)data.Distance}");
            _pushForceTextEmitter.SetForwardVector(_camera.transform.forward);
        }

        private void OnPanelSwipeReleased(SwipeData data)
        {
            if (_selectedUnit is null)
                return;

            _isMoveCompleted = false;

            var multiplier = _dataProvider.CalculateMultiplier(data.Distance);

            _targetCircle.Stop();
            _pushForceTextEmitter.Stop();

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