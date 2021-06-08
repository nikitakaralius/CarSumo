using System;
using CarSumo.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CarSumo.Input
{
    public class SwipeInputPanel : MonoBehaviour, ISwipePanel
    {
        [SerializeField] private InputSettings _settings;

        public event Action<SwipeData> Begun;

        public event Action<SwipeData> Swiping;

        public event Action<SwipeData> Released;

        private SwipeData _sendingData;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _sendingData.StartPosition = eventData.position;
            _sendingData.Delta = eventData.delta;
            _sendingData.EndPosition = eventData.position;

            Begun?.Invoke(_sendingData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _sendingData.Delta = eventData.delta;
            _sendingData.EndPosition = eventData.position;

            Swiping?.Invoke(_sendingData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _sendingData.EndPosition = eventData.position;
            _sendingData.Delta = Vector2.zero;

            Released?.Invoke(_sendingData);
        }

        public float GetAxisValue(int axis)
        {
            if (axis != 0)
                return 0.0f;

            var xAxis = _sendingData.Delta.x / _settings.SwipeDeltaDivider;

            return Mathf.Clamp(xAxis, -1.0f, 1.0f);
        }
    }
}