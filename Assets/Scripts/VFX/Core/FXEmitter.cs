using System;
using System.Collections;
using UnityEngine;

namespace CarSumo.VFX
{
    public class FXEmitter : MonoBehaviour
    {
        [SerializeField] private ParticlesFactory _factory;

        private ParticleSystem _entityInstance;

        private void Awake()
        {
            _entityInstance = _factory.Create(transform);
            _entityInstance.Stop();
        }

        public void Emit()
        {
            _entityInstance.Play();
        }

        public void Stop()
        {
            _entityInstance.Stop();
        }

        public void EmitUntil(Func<bool> predicate)
        {
            StartCoroutine(EmitUntilRoutine(predicate));
        }

        private IEnumerator EmitUntilRoutine(Func<bool> predicate)
        {
            _entityInstance.Play();

            yield return new WaitUntil(predicate);

            _entityInstance.Stop();
        }
    }
}