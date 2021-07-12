using System;
using System.Threading.Tasks;
using UnityEngine;

namespace CarSumo.Infrastructure.Services.TimerService
{
    public class CountdownTimer : ITimerService
    {
        private readonly float _startTimeRemaining;
        private bool _stopped = false;

        public CountdownTimer(float startTimeRemaining)
        {
            _startTimeRemaining = startTimeRemaining;
        }

        public event Action Elapsed;

        public float Seconds { get; private set; }

        public void Start()
        {
            Task.Run(async () =>
            {
                _stopped = false;
                Seconds = _startTimeRemaining;

                while (Seconds > 0.0f && _stopped == false)
                {
                    Seconds = Mathf.Clamp(Seconds - Time.deltaTime, 0.0f, _startTimeRemaining);
                }

                if (Seconds == 0.0f)
                    Elapsed?.Invoke();
            });
        }

        public void Stop()
        {
            _stopped = true;
            Seconds = 0.0f;
        }
    }
}