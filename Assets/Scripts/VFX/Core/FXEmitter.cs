using UnityEngine;

namespace CarSumo.VFX
{
    public class FXEmitter : MonoBehaviour
    {
        [SerializeField] private ParticlesFactory _factory;
        [SerializeField] private float _destroyDelay;

        private ParticleSystem _entityInstance;

        private void Awake()
        {
            _entityInstance = _factory.Create();
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
    }
}