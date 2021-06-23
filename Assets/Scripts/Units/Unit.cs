using CarSumo.Data;
using CarSumo.Teams;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Factory;
using UnityEngine;

namespace CarSumo.Units
{
    public class Unit : MonoBehaviour, IVehicleUpgrader, IVehicleDestroyer
    {
        [SerializeField] private Team _team;
        [SerializeField] private VehicleHierarchy _vehicleHierarchy;

        private int _generation = -1;

        private void Awake()
        {
            //todo: check -transofrm.forward if you changed the assets
            var worldPlacement = new WorldPlacement(transform.position, -transform.forward);
            CreateVehicleInstance(worldPlacement);
        }

        public void Destroy(Vehicle vehicle)
        {
            Destroy(vehicle.gameObject);
            Destroy(gameObject);
        }

        public void Upgrade(Vehicle vehicle)
        {
            if (_vehicleHierarchy.CanCreate(_generation + 1) == false)
                return;

            var transform = vehicle.transform;
            var worldPlacement = new WorldPlacement(transform.position, transform.forward);

            Destroy(vehicle.gameObject);

            CreateVehicleInstance(worldPlacement);
        }

        private void CreateVehicleInstance(WorldPlacement worldPlacement)
        {
            var factory = _vehicleHierarchy.GetVehicleFactoryByGeneration(++_generation);

            factory.Create(transform).Init(_team, worldPlacement, this, this);
        }
    }
}