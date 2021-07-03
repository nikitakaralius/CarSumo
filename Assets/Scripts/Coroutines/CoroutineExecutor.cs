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

        public void StartCoroutine(IEnumerator coroutine)
        {
            _executor.StartCoroutine(coroutine);
        }

        public void StopCoroutine(IEnumerator coroutine)
        {
            _executor.StopCoroutine(coroutine);
        }
    }
}
