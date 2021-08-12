using DG.Tweening;
using UnityEngine;

namespace TweenAnimations
{
    public class SizeTweenAnimation : MonoBehaviour
    {
        [SerializeField] private Vector3TweenData _tweenData;

        private Tween _increaseSizeTween;
        private Tween _decreaseSizeTween;

        private void OnDisable()
        {
            _increaseSizeTween?.Kill();
            _decreaseSizeTween?.Kill();
            transform.localScale = _tweenData.From;
        }

        public void IncreaseSize(Transform transform)
        {
            _increaseSizeTween = transform
                .DOScale(_tweenData.To, _tweenData.Duration)
                .SetEase(_tweenData.Ease);
        }

        public void DecreaseSize(Transform transform)
        {
            _decreaseSizeTween = transform
                .DOScale(_tweenData.From, _tweenData.Duration)
                .SetEase(_tweenData.Ease);
        }
    }
}