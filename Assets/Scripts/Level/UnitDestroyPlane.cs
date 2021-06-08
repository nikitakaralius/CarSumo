using CarSumo.Extensions;
using CarSumo.Units;
using UnityEngine;

namespace CarSumo.Level
{
    public class UnitDestroyPlane : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.HandleComponent<Unit>(handler => handler.Destroy());
        }
    }
}