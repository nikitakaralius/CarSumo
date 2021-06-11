using System;
using UnityEngine;
using System.Collections.Generic;

namespace CarSumo.Units.Factory
{
    [CreateAssetMenu(fileName = "Vehicles Hierarchy", menuName = "CarSumo/Vehicles/Hierarchy")]
    public class VehicleHierarchyFactory : ScriptableObject
    {
        public int Count => _hierarchy.Count;

        [SerializeField] private List<VehicleFactory> _hierarchy;

        public VehicleFactory GetVehicleFactoryByIndex(int index)
        {
            if (index < 0 || index >= _hierarchy.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            return _hierarchy[index];
        }

        public bool TryGetVehicleFactoryByIndex(int index, out VehicleFactory factory)
        {
            factory = null;

            if (index < 0 || index >= _hierarchy.Count)
                return false;

            factory = _hierarchy[index];
            return true;
        }
    }
}