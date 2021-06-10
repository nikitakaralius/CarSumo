using UnityEngine;
using Sirenix.OdinInspector;

namespace CarSumo.Factory
{
    public abstract class EmitterScriptableObject : SerializedScriptableObject
    {
        public abstract void Emit(Transform parent = null);
        public abstract void Stop();
    }
}