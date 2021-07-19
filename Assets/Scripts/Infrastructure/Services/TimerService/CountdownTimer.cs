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
        private Coroutine _timerRoutine;

        public CountdownTimer(float startTimeRemaining, CoroutineExecutor coroutineExecutor)
        {
            _startTimeRemaining = startTimeRemaining;
            _coroutineExecutor = coroutineExecutor;
        }

        public event Action Elapsed;

        public float Seconds { get; private set; }

        public void Start()
        {
            Stop();
            _timerRoutine = _coroutineExecutor.StartCoroutine(TimerRoutine());
        }

        public void Stop()
        {
            if (_timerRoutine is null)
                return;

            _coroutineExecutor.StopCoroutine(_timerRoutine);
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