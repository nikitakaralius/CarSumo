using CarSumo.Vehicles.Factory;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Game
{
    public class VehicleFactoryInstaller : MonoInstaller
    {
        [SerializeField] private VehicleHierarchy _vehicleHierarchy;
        [SerializeField] private VehicleFactory[] _factories;

        public override void InstallBindings()
        {
            BindVehicleHierarchy();
            InjectInVehicleFactories();
        }

        private void BindVehicleHierarchy()
        {
            Container
                .Bind<IVehicleHierarchy>()
                .FromInstance(_vehicleHierarchy)
                .AsTransient();
        }

        private void InjectInVehicleFactories()
        {
            foreach (VehicleFactory factory in _factories)
            {
                Container.QueueForInject(factory);
            }
        }
    }
}