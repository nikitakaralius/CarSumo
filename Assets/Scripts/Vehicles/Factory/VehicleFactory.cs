using CarSumo.Factory;
using CarSumo.Teams;
using UnityEngine;

namespace CarSumo.Vehicles.Factory
{
    [CreateAssetMenu(fileName = "Vehicle Factory", menuName = "CarSumo/Vehicles/Factory")]
    public class VehicleFactory : FactoryScriptableObject<Vehicle>
    {
        [SerializeField] private Vehicle _vehiclePrefab;

        public override Vehicle Create(Transform parent = null)
        {
            return Instantiate(_vehiclePrefab, parent);
        }

        public Vehicle Create(Transform parent, Team team)
        {
            var instance = Create(parent);
            instance.Init(team);
            return instance;
        }
    }
}