using System;
using DG.Tweening;
using UnityEngine;

namespace TweenAnimations
{
    public class LoopedSizeTweenAnimation : MonoBehaviour, ITweenAnimation
    {
        [SerializeField] private Vector3TweenData _data;
        [SerializeField] private bool _rememberInitialScale = true;
        
        private Tweener _tweener;

        private void Start()
        {
            if (_rememberInitialScale)
                _data.From = transform.localScale;
        }

        private void OnDestroy()
        {
            Stop();
        }

        public void Play()
        {
            _tweener = transform
                .DOScale(_data.To, _data.Duration)
                .SetEase(_data.Ease)
                .SetLoops(-1, LoopType.Yoyo)
                .OnKill(() => transform.localScale = _data.From);
        }

        public void Stop()
        {
            _tweener?.Kill();
        }
    }
}