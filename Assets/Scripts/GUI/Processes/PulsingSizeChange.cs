using System.Collections.Generic;
using CarSumo.GUI.Processees;
using CarSumo.Structs;
using DG.Tweening;
using UnityEngine;

namespace CarSumo.GUI.Processes
{
    public class PulsingSizeChange : DOTweenProcess
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private TweenData<Vector3> _data;
        
        public override void Init()
        {
        }

        public override void OnApplied()
        {
        }

        protected override IEnumerable<Tween> CreateTweenSection()
        {
           return new [] {_rectTransform.DOScale(_data.Range.Max, _data.Duration)
                                        .SetEase(_data.Ease)
                                        .SetLoops(-1, LoopType.Yoyo)
           };
        }
    }
}