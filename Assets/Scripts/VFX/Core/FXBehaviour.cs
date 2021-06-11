using UnityEngine;

namespace CarSumo.VFX
{
    public class FXBehaviour : MonoBehaviour
    {
        [SerializeField] private ParticlesFactory _factory;
        [SerializeField] private float _destroyDelay;

        private ParticleSystem _entityInstance;

        public void Emit()
        {
            _entityInstance = _factory.Create(transform);
            _entityInstance.Play();
        }

        public void Stop()
        {
            _entityInstance.Stop();
            Destroy(_entityInstance.gameObject, _destroyDelay);
        }
    }
}