#if UNITY_EDITOR

using CarSumo.Input;
using UnityEngine;
using Sirenix.OdinInspector;

namespace CarSumo.Debugging
{
    public class SwipeInputPanelDebugger : SerializedMonoBehaviour
    {
        [SerializeField] private ISwipePanel _panel;

        private void OnEnable()
        {
            _panel.Swiping += OnPanelSwiping;
            _panel.Released += OnPanelSwipeReleased;
        }

        private void OnDisable()
        {
            _panel.Swiping -= OnPanelSwiping;
            _panel.Released -= OnPanelSwipeReleased;
        }

        private void OnPanelSwipeReleased(SwipeData data)
            => PrintSwipeInfo(data, "Released");

        private void OnPanelSwiping(SwipeData data)
            => PrintSwipeInfo(data, "Swiping");

        private void PrintSwipeInfo(SwipeData data, string state)
        {
            Debug.Log($"{state}\n" +
                      $"Axis Value: {_panel.GetAxisValue(0)}" +
                      $"Delta: {data.Delta}\n" +
                      $"Start Position: {data.StartPosition}\n" +
                      $"End Position {data.EndPosition}");
        }
    }
}

#endif