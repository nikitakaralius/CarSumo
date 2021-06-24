using UnityEngine;

namespace CarSumo.Vehicles.Factory
{
    [CreateAssetMenu(fileName = "Vehicle Hierarchy", menuName = "CarSumo/Vehicles/Hierarchy")]
    public class VehicleHierarchy : ScriptableObject
    {
        [SerializeField] private VehicleFactory[] _factoriesHierarchy;

        public bool CanCreate(int generation)
        {
            return generation < _factoriesHierarchy.Length;
        }

        public VehicleFactory GetVehicleFactoryByGeneration(int generation)
        {
            if (TryGetVehicleFactoryByGeneration(generation, out var factory) == false)
                throw new System.ArgumentOutOfRangeException(nameof(generation));

            return factory;
        }

        public bool TryGetVehicleFactoryByGeneration(int generation, out VehicleFactory factory)
        {
            factory = null;

            if (generation >= _factoriesHierarchy.Length || generation < 0)
                return false;

            factory = _factoriesHierarchy[generation];
            return true;
        }
    }
}
