using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CarSumo.Input
{
    public class SwipeInputPanel : MonoBehaviour, ISwipePanel
    {
        [SerializeField] private float _deltaDivider = 10;

        public event Action<SwipeData> Swiping;

        public event Action<SwipeData> Released;

        private SwipeData _data;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _data.StartPosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _data.Delta = eventData.delta;
            _data.EndPosition = eventData.position;

            Swiping?.Invoke(_data);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _data.EndPosition = eventData.position;
            _data.Delta = Vector2.zero;

            Released?.Invoke(_data);
        }

        public float GetAxisValue(int axis)
        {
            if (axis < 0 || axis > 2)
                throw new ArgumentOutOfRangeException(nameof(axis));

            if (axis != 1)
                throw new NotImplementedException(nameof(axis));

            var xAxis = _data.Delta.x / _deltaDivider;

            return Mathf.Clamp(xAxis, -1.0f, 1.0f);
        }
    }
}