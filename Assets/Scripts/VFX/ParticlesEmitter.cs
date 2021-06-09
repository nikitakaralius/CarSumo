using CarSumo.Factory;
using UnityEngine;

namespace CarSumo.VFX
{
    [CreateAssetMenu(fileName = "Particles Emitter", menuName = "CarSumo/VFX/Particles Emitter")]
    public class ParticlesEmitter : EmitterScriptableObject
    {
        [SerializeField] private ParticlesFactory _factory;
        [SerializeField] private float _destroyDelay;

        private ParticleSystem _particlesInstance;

        public override void Emit()
        {
            _particlesInstance = _factory.Create();
        }
        public void Emit(Transform parent)
        {
            _particlesInstance = _factory.Create(parent);
        }

        public override void Stop()
        {
            _particlesInstance.Stop();
            Destroy(_particlesInstance, _destroyDelay);
        }
    }
}