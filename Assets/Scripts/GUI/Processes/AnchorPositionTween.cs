using System.Collections.Generic;
using CarSumo.Structs;
using DG.Tweening;
using UnityEngine;

namespace CarSumo.GUI.Processees
{
    public class AnchorPositionTween : DoTweenProcess
    {
        [SerializeField] private RectTransform[] _rectTransforms = new RectTransform[0];
        [SerializeField] private TweenData<Vector2> _data;

        private TweenData<Vector2> _manageableData;

        public AnchorPositionTween(RectTransform[] rectTransforms, TweenData<Vector2> data)
        {
            _rectTransforms = rectTransforms;
            _data = data;

            Init();
        }

        public override void Init()
        {
            _manageableData = _data;
        }

        public override void OnApplied()
        {
            _manageableData = _manageableData.Inverted;
        }
        
        protected override IEnumerable<Tween> CreateTweenSection()
        {
            Tween[] tweens = new Tween[_rectTransforms.Length];

            for (var i = 0; i < _rectTransforms.Length; i++)
            {
                RectTransform rectTransform = _rectTransforms[i];
                Tween tween = ChangeRectPosition(rectTransform, _manageableData.Range.Max, _manageableData.Duration)
                    .SetEase(_data.Ease);
                tweens[i] = tween;
            }

            return tweens;
        }

        private Tweener ChangeRectPosition(RectTransform rectTransform, Vector2 endValue, float duration)
        {
            return DOTween.To(() => rectTransform.anchoredPosition,
                             anchoredPosition => rectTransform.anchoredPosition = anchoredPosition,
                             endValue,
                             duration);
        }
    }
}
