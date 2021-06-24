using UnityEngine;

namespace CarSumo
{
    public class WaitForSmallDelay : CustomYieldInstruction
    {
        private readonly float _startTime;
        private const float _delay = 0.2f;

        public WaitForSmallDelay()
        {
            _startTime = Time.time;
        }

        public override bool keepWaiting => Time.time < _startTime + _delay;
    }
}