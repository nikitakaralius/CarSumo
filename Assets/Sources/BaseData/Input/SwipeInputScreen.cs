using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CarSumo.Input
{
    public class SwipeInputScreen : MonoBehaviour, ISwipeScreen
    {
        public event Action<Swipe> Begun;

        public event Action<Swipe> Swiping;

        public event Action<Swipe> Released;

        private Vector2 _startPosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = eventData.position;

            var sendingData = new Swipe(_startPosition, eventData.position, eventData.delta);

            Begun?.Invoke(sendingData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var sendingData = new Swipe(_startPosition, eventData.position, eventData.delta);

            Swiping?.Invoke(sendingData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var sendingData = new Swipe(_startPosition, eventData.position, eventData.delta);

            Released?.Invoke(sendingData);
        }
    }
}