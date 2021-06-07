using CarSumo.Extensions;
using UnityEngine;
using CarSumo.Input;
using Sirenix.OdinInspector;

namespace CarSumo.Units
{
    public class UnitSelector : SerializedMonoBehaviour
    {
        [SerializeField] private ISwipePanel _panel;
        [SerializeField] private Camera _camera;

        private void OnEnable()
        {
            _panel.Begun += OnPanelSwipeBegun;
        }

        private void OnDisable()
        {
            _panel.Begun -= OnPanelSwipeBegun;
        }

        private void OnPanelSwipeBegun(SwipeData data)
        {
            var ray = _camera.ScreenPointToRay(data.EndPosition);

            if (Physics.Raycast(ray, out var hit) == false)
                return;

            if (hit.collider.HasComponent<Unit>() == false)
                return;

            Debug.Log(hit.collider.name);
        }
    }
}