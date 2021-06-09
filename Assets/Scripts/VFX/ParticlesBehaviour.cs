using UnityEngine;

namespace CarSumo.VFX
{
    public class ParticlesBehaviour : MonoBehaviour
    {
        [SerializeField] private ParticlesEmitter _emitter;

        public void Emit() => _emitter.Emit(transform);

        public void Stop() => _emitter.Stop();
    }
}