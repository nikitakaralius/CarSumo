using System.Collections;
using UnityEngine;

namespace CarSumo.Coroutines
{
    public class CoroutineExecutor
    {
        private readonly MonoBehaviour _executor;

        public CoroutineExecutor(MonoBehaviour executor)
        {
            _executor = executor;
        }

        public Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return _executor.StartCoroutine(coroutine);
        }

        public void StopCoroutine(Coroutine coroutine)
        {
            _executor.StopCoroutine(coroutine);
        } 
    }
}
