using System;
using System.Collections;
using CarSumo.Coroutines;
using UnityEngine;

namespace CarSumo.Infrastructure.Services.TimerService
{
    public class CountdownTimer : ITimerService
    {
        private readonly float _startTimeRemaining;
        private readonly CoroutineExecutor _coroutineExecutor;

        public CountdownTimer(float startTimeRemaining, CoroutineExecutor coroutineExecutor)
        {
            _startTimeRemaining = startTimeRemaining;
            _coroutineExecutor = coroutineExecutor;
        }

        public event Action Elapsed;

        public float Seconds { get; private set; }

        public void Start()
        {
            _coroutineExecutor.StartCoroutine(TimerRoutine());
        }

        public void Stop()
        {
            _coroutineExecutor.StopCoroutine(TimerRoutine());
        }

        private IEnumerator TimerRoutine()
        {
            Seconds = _startTimeRemaining;

            while (Seconds > 0.0f)
            {
                Seconds = Mathf.Clamp(Seconds - Time.deltaTime, 0.0f, _startTimeRemaining);
                yield return null;
            }

            if (Seconds == 0.0f)
                Elapsed?.Invoke();
        }
        
    }
}