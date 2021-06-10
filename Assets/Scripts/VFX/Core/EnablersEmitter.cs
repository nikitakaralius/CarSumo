using CarSumo.Abstract;
using CarSumo.Factory;
using UnityEngine;

namespace CarSumo.VFX
{
    [CreateAssetMenu(fileName = "Enablers Emitter", menuName = "CarSumo/VFX/Enablers Emitter")]
    public class EnablersEmitter : EmitterScriptableObject
    {
        [SerializeField] private EnablersFactory _enablerFactory;

        private Enabler _enabler;

        public override void Emit(Transform parent = null)
        {
            _enabler = _enablerFactory.Create(parent);
            _enabler.Enable();
        }

        public override void Stop()
        {
            _enabler.Disable();
        }
    }
}