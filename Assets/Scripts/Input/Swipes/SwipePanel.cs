using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CarSumo.Input.Swipes
{
    public class SwipePanel : MonoBehaviour, ISwipePanel
    {
        public event Action<SwipeData> Swiping;

        public event Action<SwipeData> Released;

        private Vector2 _startPosition;

        private float _dragTime;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = eventData.position;

            _dragTime += Time.deltaTime;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var data = CreateSwipeData(eventData.delta, eventData.position);
            Swiping?.Invoke(data);

            _dragTime += Time.deltaTime;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var data = CreateSwipeData(eventData.delta, eventData.position);
            Released?.Invoke(data);

            _dragTime = 0.0f;
        }

        private SwipeData CreateSwipeData(Vector2 delta, Vector2 endPosition)
        {
            return new SwipeData
            {
                Delta = delta,
                StartPosition = _startPosition,
                EndPosition = endPosition,
                DragTime = _dragTime
            };
        }
    }
}