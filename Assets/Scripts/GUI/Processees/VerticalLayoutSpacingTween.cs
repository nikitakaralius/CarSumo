using CarSumo.Structs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CarSumo.GUI.Processes
{
    public class VerticalLayoutSpacingTween : ITweenProcess
    {
        [SerializeField] private VerticalLayoutGroup _group;
        [SerializeField] private TweenData _data;

        private TweenData _manageableData;

        public void Init()
        {
            _manageableData = _data;
        }

        public void ApplyProcess()
        {
            TranslateSpacing(_manageableData);
            _manageableData = _manageableData.Inverted;
        }

        public void TranslateSpacing(TweenData tweenData)
        {
            TranslateSpacing(tweenData.Range, tweenData.Duration, tweenData.Ease);
        }

        public void TranslateSpacing(Range range, float duration, Ease ease)
        {
            DOTween.To(() => _group.spacing, spacing => _group.spacing = spacing, range.Max, duration).SetEase(ease);
        }
    }
}
