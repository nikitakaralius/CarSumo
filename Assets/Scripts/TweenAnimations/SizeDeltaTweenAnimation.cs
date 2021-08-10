using DG.Tweening;
using UnityEngine;

namespace TweenAnimations
{
    public class SizeDeltaTweenAnimation : MonoBehaviour
    {
        [SerializeField] private Vector3 _delta;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        private Vector3 _initialSize;

        private Tween _animation;

        private void Start()
        {
            _initialSize = transform.localScale;
        }

        private void OnDisable()
        {
            _animation?.Kill();
            transform.localScale = _initialSize;
        }

        public void IncreaseSize()
        {
            _animation?.Kill();
            
            _animation = transform.DOScale(_initialSize + _delta, _duration)
                .SetEase(_ease)
                .OnKill(() => transform.localScale = _initialSize + _delta);
        }

        public void DecreaseSize()
        {
            _animation?.Kill();
            
            _animation = transform.DOScale(_initialSize, _duration)
                .SetEase(_ease)
                .OnKill(() => transform.localScale = _initialSize);
        }
    }
}