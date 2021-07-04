using System;
using UnityEngine;

namespace CarSumo
{
    [Serializable]
    public struct Range
    {
        [SerializeField] private float _min;
        [SerializeField] private float _max;

        public Range(float min, float max)
        {
            _min = min;
            _max = max;
        }

        public float Min => _min;

        public float Max => _max;

        public Range Inverted => new Range(_max, _min);

        public static implicit operator Range(Range<float> value)
        {
            return new Range(value.Min, value.Max);
        }
    }

    [Serializable]
    public struct Range<T>
    {
        [SerializeField] private T _min;
        [SerializeField] private T _max;

        public Range(T min, T max)
        {
            _min = min;
            _max = max;
        }

        public T Min => _min;

        public T Max => _max;

        public Range<T> Inverted => new Range<T>(_max, _min);

        public static implicit operator Range<T>(Range value)
        {
            return new Range(value.Min, value.Max);
        }
    }
}
