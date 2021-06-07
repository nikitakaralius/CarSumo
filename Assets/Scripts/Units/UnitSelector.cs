using CarSumo.Data;
using CarSumo.Extensions;
using UnityEngine;
using CarSumo.Input;
using Cinemachine.Utility;
using Sirenix.OdinInspector;

namespace CarSumo.Units
{
    public class UnitSelector : SerializedMonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ISwipePanel _panel;
        [SerializeField] private Camera _camera;

        [SerializeField] private UnitSelectorDataProvider _dataProvider;

        private Unit _selectedUnit;

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

            _selectedUnit = unit;
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
        }

        private void OnPanelSwipeReleased(SwipeData data)
        {
            if (_selectedUnit is null)
                return;

            var multiplier = _dataProvider.CalculateMultiplier(data.Distance);

            _selectedUnit.Push(multiplier);
            _selectedUnit = null;
        }
    }
}