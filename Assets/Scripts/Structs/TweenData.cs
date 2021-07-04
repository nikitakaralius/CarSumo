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

        public Range<T> Range => _range;

        public float Duration => _duration;

        public Ease Ease => _ease;
    }

    [Serializable]
    public struct TweenData
    {
        [SerializeField] private Range _range;

        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        public Range Range => _range;

        public float Duration => _duration;

        public Ease Ease => _ease;
    }
}
