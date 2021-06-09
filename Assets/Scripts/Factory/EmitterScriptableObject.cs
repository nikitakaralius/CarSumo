using UnityEngine;

namespace CarSumo.Factory
{
    public abstract class EmitterScriptableObject : ScriptableObject
    {
        public abstract void Emit();
        public abstract void Stop();
    }
}