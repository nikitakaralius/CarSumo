using CarSumo.Factory;
using CarSumo.Units.Stats;
using UnityEngine;

namespace CarSumo.Units.Factory
{
    public class VehicleFactory : FactoryScriptableObject<Vehicle>
    {
        [SerializeField] private Vehicle _vehiclePrefab;

        public override Vehicle Create(Transform parent = null)
        {
            return Instantiate(_vehiclePrefab, parent);
        }

        public Vehicle Create(Transform parent, IVehicleStatsProvider statsProvider)
        {
            var instance = Create(parent);
            instance.Init(statsProvider);
            return instance;
        }
    }
}