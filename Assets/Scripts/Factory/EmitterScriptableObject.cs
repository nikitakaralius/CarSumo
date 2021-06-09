using UnityEngine;

namespace CarSumo.Factory
{
    public abstract class EmitterScriptableObject : ScriptableObject
    {
        public abstract void Emit(Transform parent = null);
        public abstract void Stop();
    }
}