using UnityEngine;
using System.Collections.Generic;

namespace CarSumo.Units.Factory
{
    [CreateAssetMenu(fileName = "Vehicles Hierarchy", menuName = "CarSumo/Vehicles/Hierarchy")]
    public class VehicleHierarchyFactory : ScriptableObject
    {
        [SerializeField] private List<VehicleFactory> _hierarchy;

        public VehicleFactory ClampedGetVehicleFactoryByIndex(int index)
        {
            int clampedIndex = Mathf.Clamp(index, 0, _hierarchy.Count - 1);
            return GetVehicleFactoryByIndex(clampedIndex);
        }

        public VehicleFactory GetVehicleFactoryByIndex(int index)
        {
            if (index < 0 || index >= _hierarchy.Count)
                throw new System.ArgumentOutOfRangeException(nameof(index));

            return _hierarchy[index];
        }
    }
}