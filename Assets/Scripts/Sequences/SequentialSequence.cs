using UnityEngine;

namespace CarSumo.Sequences
{
    public class SequentialSequence : ISequence
    {
        private readonly int _previousIndex;
        private readonly int _maxValue;

        public SequentialSequence(int previousIndex, int maxValue)
        {
            _previousIndex = previousIndex;
            _maxValue = maxValue;
        }

        public int Next()
        {
            return (int)Mathf.PingPong(_previousIndex + 1, _maxValue);
        }
    }
}