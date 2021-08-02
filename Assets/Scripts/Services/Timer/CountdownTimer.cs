using System;
using System.Collections;
using UnityEngine;

namespace Services.Timer
{
    public class CountdownTimer : MonoBehaviour, ITimer
    {
        private Coroutine _timerRoutine;
        
        public event Action Elapsed;
        
        public float SecondsLeft { get; private set; }

        public void Start(float secondsToElapse)
        {
            Stop();
            _timerRoutine = StartCoroutine(StartTimer(secondsToElapse));
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