using UnityEngine;
using CarSumo.Input.Swipes;
using Sirenix.OdinInspector;

namespace CarSumo.Input.Debugging
{
    public class SwipePanelDebugger : SerializedMonoBehaviour
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
                      $"Delta: {data.Delta}\n" +
                      $"Drag Time: {data.DragTime}\n" +
                      $"Start Position: {data.StartPosition}\n" +
                      $"End Position {data.EndPosition}");
        }
    }
}