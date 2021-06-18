using UnityEngine;

namespace CarSumo.Sequences
{
    public class RandomNoImmediateRepeatSequence : ISequence
    {
        private readonly int _previousIndex;
        private readonly int _maxValue;

        public RandomNoImmediateRepeatSequence(int previousIndex, int maxValue)
        {
            _previousIndex = previousIndex;
            _maxValue = maxValue;
        }

        public int Next()
        {
            int chosenIndex = -1;

            do
            {
                chosenIndex = Random.Range(0, _maxValue);
            } 
            while (chosenIndex == _previousIndex);

            return chosenIndex;
        }
    }
}