using CarSumo.Data;
using CarSumo.Teams;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Factory;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CarSumo.Units
{
    public class Unit : MonoBehaviour, IVehicleUpgrader, IVehicleDestroyer
    {
        [SerializeField] private Team _team;
        
        private VehicleHierarchy _vehicleHierarchy;
        private IUnitTracker _unitTracker;
        private int _generation = -1;

        public Team Team => _team;

        [Inject]
        private void Construct(IUnitTracker unitTracker, VehicleHierarchy vehicleHierarchy)
        {
            _unitTracker = unitTracker;
            _vehicleHierarchy = vehicleHierarchy;
        }

        private void Awake()
        {
            _unitTracker.Add(this);
            
            var worldPlacement = new WorldPlacement(transform.position, -transform.forward);
            CreateVehicleInstance(worldPlacement);
        }

        public void Destroy(Vehicle vehicle)
        {
            _unitTracker.Remove(this);

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