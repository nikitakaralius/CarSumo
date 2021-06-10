using CarSumo.Extensions;
using CarSumo.Storage;
using CarSumo.Units;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarSumo.Level
{
    public class UnitDestroyPlane : SerializedMonoBehaviour
    {
        [SerializeField] private IReactiveUnitStorage _storage;

        private void OnTriggerEnter(Collider other)
        {
            other.HandleComponent<Vehicle>(vehicle =>
            {
                vehicle.Destroy();
            });
        }
    }
}