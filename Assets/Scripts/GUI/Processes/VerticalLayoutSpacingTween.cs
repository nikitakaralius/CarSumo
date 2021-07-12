using System.Collections.Generic;
using CarSumo.GUI.Processees;
using CarSumo.Structs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CarSumo.GUI.Processes
{
    public class VerticalLayoutSpacingTween : DoTweenProcess
    {
        [SerializeField] private VerticalLayoutGroup _group;
        [SerializeField] private TweenData _data;

        private TweenData _manageableData;

        public VerticalLayoutSpacingTween(VerticalLayoutGroup group, TweenData data)
        {
            _group = group;
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
            return new[] {TranslateSpacing(_manageableData)};
        }

        private Tween TranslateSpacing(TweenData tweenData)
        {
            return TranslateSpacing(tweenData.Range, tweenData.Duration, tweenData.Ease);
        }

        private Tween TranslateSpacing(Range range, float duration, Ease ease)
        {
            return DOTween.To(() => _group.spacing, spacing => _group.spacing = spacing, range.Max, duration)
                .SetEase(ease);
        }
    }
}