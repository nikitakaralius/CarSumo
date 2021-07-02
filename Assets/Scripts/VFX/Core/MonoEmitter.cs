using UnityEngine;

namespace CarSumo.VFX.Core
{
    public abstract class MonoEmitter : MonoBehaviour
    {
        public abstract void Emit();

        public abstract void Stop();
    }
}
