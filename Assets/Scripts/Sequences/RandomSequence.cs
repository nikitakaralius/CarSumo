using UnityEngine;

namespace CarSumo.Sequences
{
    public class RandomSequence : ISequence
    {
        private readonly int _maxValue;

        public RandomSequence(int maxValue)
        {
            _maxValue = maxValue;
        }


        public int Next()
        {
            return Random.Range(0, _maxValue);
        }
    }
}