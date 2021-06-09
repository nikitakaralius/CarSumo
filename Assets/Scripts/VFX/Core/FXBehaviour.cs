using CarSumo.Factory;
using UnityEngine;

namespace CarSumo.VFX
{
    public class FXBehaviour : MonoBehaviour
    {
        [SerializeField] private EmitterScriptableObject _emitter;

        public void Emit() => _emitter.Emit(transform);

        public void Stop() => _emitter.Stop();
    }
}