using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CarSumo.Structs;

namespace CarSumo.GUI
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public abstract class VerticalLayoutDropdown : MonoBehaviour
    {
        private VerticalLayoutGroup _layoutGroup;

        private void Awake()
        {
            _layoutGroup = GetComponent<VerticalLayoutGroup>();
        }

        public void TranslateSpacing(TweenData<float> tweenData)
        {
            TranslateSpacing(tweenData.Range, tweenData.Duration, tweenData.Ease);
        }

        public void TranslateSpacing(Range range, float duration, Ease ease)
        {
            DOTween.To(spacing => _layoutGroup.spacing = spacing, range.Min, range.Max, duration).SetEase(ease);
        }
    }
}