using DG.Tweening;
using TweenAnimations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Menu.Animations
{
    public class DragItemAnimation : MonoBehaviour,
        IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Vector3TweenData _tweenData;

        private Tween _beginDragTween;
        private Tween _endDragTween;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _beginDragTween = transform
                .DOScale(_tweenData.To, _tweenData.Duration)
                .SetEase(_tweenData.Ease);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _endDragTween = transform
                .DOScale(_tweenData.From, _tweenData.Duration)
                .SetEase(_tweenData.Ease);
        }

        private void OnDisable()
        {
            _beginDragTween?.Kill();
            _endDragTween?.Kill();
            transform.localScale = _tweenData.From;
        }
    }
}