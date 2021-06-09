using UnityEngine;

namespace CarSumo.Factory
{
    public abstract class FactoryScriptableObject<T> : ScriptableObject where T : Component
    {
        public abstract T Create();
    }
}