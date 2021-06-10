using System;
using CarSumo.Data;
using CarSumo.Extensions;
using CarSumo.Factory;
using UnityEngine;
using CarSumo.Input;
using CarSumo.Teams;
using CarSumo.VFX;
using Cinemachine.Utility;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

namespace CarSumo.Units
{
    public class UnitSelector : SerializedMonoBehaviour, ITeamChangeSender
    {
        public event Action ChangePerformed;

        [SerializeField] private UnitSelectorDataProvider _dataProvider;

        [Header("Components")]
        [SerializeField] private ISwipePanel _panel;
        [SerializeField] private ITeamChangeHandler _teamChangeHandler;
        [SerializeField] private Camera _camera;

        [Header("FX")]
        [SerializeField] private Text3DEmitter _pushForceTextEmitter;

        [InfoBox("Unit Selected Emitters require to copy particles from other FX fields")]
        [SerializeField] private EmitterScriptableObject[] _unitSelectedEmitters;
        
        private Unit _selectedUnit;

        private bool _isMoveCompleted = true;
        private bool _pushCanceled;

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
            if (_camera.TryGetComponentWithRaycast(data.EndPosition, out Unit unit) == false)
                return;

            if (CanPickUnit(unit) == false)
                return;

            _selectedUnit = unit;
            _unitSelectedEmitters.ForEach(emitter => emitter.Emit(_selectedUnit.transform));
        }

        private void OnPanelSwiping(SwipeData data)
        {
            if (_selectedUnit is null)
                return;

            var pushPercentage = _dataProvider.CalculatePercentage(data.Distance);
            _pushCanceled = pushPercentage <= _dataProvider.CancelDistancePercent;

            _pushForceTextEmitter.SetText((int)pushPercentage);
            _pushForceTextEmitter.SetForwardVector(_camera.transform.forward);

            if (data.Distance <= _dataProvider.MinSelectDistance)
                return;

            var transformedDirection = GetTransformedDirection(data.Direction);
            _selectedUnit.Rotate(transformedDirection);
        }

        private void OnPanelSwipeReleased(SwipeData data)
        {
            if (_selectedUnit is null)
                return;

            _isMoveCompleted = false;
            _unitSelectedEmitters.ForEach(emitter => emitter.Stop());

            if (_pushCanceled)
            {
                CompleteMove();
                return;
            }

            _selectedUnit.ChangePerformed += InvokeTeamChangeRequest;
            var multiplier = _dataProvider.CalculateAccelerationMultiplier(data.Distance);
            _selectedUnit.Push(multiplier);
        }

        private void InvokeTeamChangeRequest()
        {
            ChangePerformed?.Invoke();
            _selectedUnit.ChangePerformed -= InvokeTeamChangeRequest;
            CompleteMove();
        }

        private void CompleteMove()
        {
            _selectedUnit = null;
            _isMoveCompleted = true;
        }

        private bool CanPickUnit(Unit unit)
        {
            return unit.Team == _teamChangeHandler.Team
                   && _isMoveCompleted;
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