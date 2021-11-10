using System;
using System.Collections;
using CarSumo.Coroutines;
using UniRx;
using UnityEngine;
using Zenject;

namespace Services.Timer.InGameTimer
{
    public class CountdownTimer : ITimer, ITimerOperations, IConfiguredTimerOperations, IInitializable
    {
        private readonly float _defaultSecondsToElapse;
        private readonly CoroutineExecutor _coroutineExecutor;
        private readonly ReactiveProperty<float> _secondsLeft;
        private Coroutine _timerRoutine;

        public CountdownTimer(float defaultSecondsToElapse, CoroutineExecutor coroutineExecutor)
        {
            _defaultSecondsToElapse = defaultSecondsToElapse;
            _coroutineExecutor = coroutineExecutor;
            _secondsLeft = new ReactiveProperty<float>(defaultSecondsToElapse);
        }

        public IReadOnlyReactiveProperty<float> SecondsLeft => _secondsLeft;
        public event Action Elapsed;

        public void Start(float secondsToElapse)
        {
            Stop();
            _timerRoutine = _coroutineExecutor.StartCoroutine(StartTimer(secondsToElapse));
        }

        public void Initialize()
        {
            Start();
        }

        public void Start()
        {
            Start(_defaultSecondsToElapse);
        }

        public void Stop()
        {
            if (_timerRoutine is null)
                return;
            
            _coroutineExecutor.StopCoroutine(_timerRoutine);
        }

        private IEnumerator StartTimer(float secondsToElapse)
        {
            _secondsLeft.Value = secondsToElapse;

            while (_secondsLeft.Value > 0.0f)
            {
                _secondsLeft.Value = Mathf.Clamp(_secondsLeft.Value - Time.deltaTime, 0.0f, secondsToElapse);
                yield return null;
            }
            
            Elapsed?.Invoke();
        }
    }
}