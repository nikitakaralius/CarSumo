using CarSumo.Structs;
using DG.Tweening;
using UnityEngine;

namespace CarSumo.GUI.Processees
{
    public class AnchorPositionTween : IGUIProcess
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

        public void Init()
        {
            _manageableData = _data;
        }

        public void ApplyProcess()
        {
            foreach (RectTransform rectTransform in _rectTransforms)
            {
                ChangeRectPosition(rectTransform, _manageableData.Range.Max, _manageableData.Duration)
                    .SetEase(_data.Ease);
            }

            _manageableData = _manageableData.Inverted;
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
