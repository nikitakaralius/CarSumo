using UnityEngine;

namespace CarSumo.Abstract
{
    public abstract class Enabler : MonoBehaviour
    {
        public abstract void Enable();

        public abstract void Disable();
    }
}