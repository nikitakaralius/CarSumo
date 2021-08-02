using System;
using System.Collections;
using UnityEngine;

namespace Services.Timer
{
    public class CountdownTimer : MonoBehaviour, ITimer, ITimerOperations, IConfiguredTimerOperations
    {
        private readonly float _defaultSecondsToElapse;
        private Coroutine _timerRoutine;

        public CountdownTimer(float defaultSecondsToElapse)
        {
            _defaultSecondsToElapse = defaultSecondsToElapse;
        }
        
        public event Action Elapsed;
        
        public float SecondsLeft { get; private set; }

        public void Start(float secondsToElapse)
        {
            Stop();
            _timerRoutine = StartCoroutine(StartTimer(secondsToElapse));
        }

        public void Start()
        {
            Start(_defaultSecondsToElapse);
        }

        public void Stop()
        {
            if (_timerRoutine is null)
                return;
            
            StopCoroutine(_timerRoutine);
        }

        private IEnumerator StartTimer(float secondsToElapse)
        {
            SecondsLeft = secondsToElapse;

            while (SecondsLeft > 0.0f)
            {
                SecondsLeft = Mathf.Clamp(SecondsLeft - Time.deltaTime, 0.0f, secondsToElapse);
                yield return null;
            }
            
            Elapsed?.Invoke();
        }
    }
}