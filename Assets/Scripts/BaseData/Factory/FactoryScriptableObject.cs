using UnityEngine;
using Sirenix.OdinInspector;

namespace CarSumo.Factory
{
    public abstract class FactoryScriptableObject<T> : SerializedScriptableObject where T : Object
    {
        public abstract T Create(Transform parent = null);
    }
}