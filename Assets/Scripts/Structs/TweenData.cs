using System;
using DG.Tweening;
using UnityEngine;

namespace CarSumo.Structs
{
    [Serializable]
    public struct TweenData<T>
    {
        [SerializeField] private Range<T> _range;

        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        public TweenData(Range<T> range, float duration, Ease ease)
        {
            _range = range;
            _duration = duration;
            _ease = ease;
        }

        public Range<T> Range => _range;

        public float Duration => _duration;

        public Ease Ease => _ease;

        public TweenData<T> Inverted => new TweenData<T>(Range.Inverted, Duration, Ease);


        public static implicit operator TweenData<T>(TweenData value)
        {
            return new TweenData(value.Range, value.Duration, value.Ease);
        }
    }

    [Serializable]
    public struct TweenData
    {
        [SerializeField] private Range _range;

        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        public TweenData(Range range, float duration, Ease ease)
        {
            _range = range;
            _duration = duration;
            _ease = ease;
        }

        public Range Range => _range;

        public float Duration => _duration;

        public Ease Ease => _ease;

        public TweenData Inverted => new TweenData(Range.Inverted, Duration, Ease);

        public static implicit operator TweenData(TweenData<float> value)
        {
            return new TweenData(value.Range, value.Duration, value.Ease);
        }
    }
}
