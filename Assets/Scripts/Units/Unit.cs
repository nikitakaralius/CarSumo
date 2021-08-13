using System.Threading.Tasks;
using CarSumo.Structs;
using CarSumo.Teams;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Factory;
using UnityEngine;
using Zenject;

namespace CarSumo.Units
{
    public class Unit : MonoBehaviour, IVehicleDestroyer
    {
        [SerializeField] private Team _team;
        
        private IVehicleHierarchy _vehicleHierarchy;
        private IUnitTracker _unitTracker;
        private int _generation = -1;

        public Team Team => _team;

        [Inject]
        private void Construct(IUnitTracker unitTracker, IVehicleHierarchy vehicleHierarchy)
        {
            _unitTracker = unitTracker;
            _vehicleHierarchy = vehicleHierarchy;
        }

        public async Task Initialize()
        {
            _unitTracker.Add(this);
            
            var worldPlacement = new WorldPlacement(transform.position, -transform.forward);
            await CreateVehicleInstance(worldPlacement);
        }

        public void Destroy(Vehicle vehicle)
        {
            _unitTracker.Remove(this);

            Destroy(vehicle.gameObject);
            Destroy(gameObject);
        }

        private async Task CreateVehicleInstance(WorldPlacement worldPlacement)
        {
            var factory = _vehicleHierarchy.GetVehicleFactoryByGeneration(_team, ++_generation);

            Vehicle vehicle = await factory.Create(transform);
            vehicle.Initialize(_team, worldPlacement, this, this);
        }
    }
}