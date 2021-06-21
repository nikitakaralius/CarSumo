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

        private Vector2 _startPosition;
        private Vector2 _delta;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = eventData.position;

            var sendingData = new SwipeData(_startPosition, eventData.position, eventData.delta);
            _delta = eventData.delta;

            Begun?.Invoke(sendingData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var sendingData = new SwipeData(_startPosition, eventData.position, eventData.delta);
            _delta = eventData.delta;

            Swiping?.Invoke(sendingData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var sendingData = new SwipeData(_startPosition, eventData.position, eventData.delta);
            _delta = eventData.delta;

            Released?.Invoke(sendingData);
        }

        public float GetAxisValue(int axis)
        {
            if (axis != 0)
                return 0.0f;

            var xAxis = _delta.x / _settings.SwipeDeltaDivider;

            return Mathf.Clamp(xAxis, -1.0f, 1.0f);
        }
    }
}