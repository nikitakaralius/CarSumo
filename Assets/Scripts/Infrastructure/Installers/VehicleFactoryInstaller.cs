using CarSumo.Infrastructure.Services.Instantiate;
using CarSumo.Vehicles.Factory;
using UnityEngine;
using Zenject;

namespace CarSumo.Infrastructure.Installers
{
    public class VehicleFactoryInstaller : MonoInstaller
    {
        [SerializeField] private VehicleFactory[] _factories;
        [SerializeField] private VehicleHierarchy _vehicleHierarchy;

        public override void InstallBindings()
        {
            BindInstantiateService();
            InjectInVehicleFactories();
            BindVehicleHierarchy();
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

        private void BindInstantiateService()
        {
            Container
                .Bind<IAddressablesInstantiate>()
                .FromInstance(new ZenjectAddressablesInstantiate(Container))
                .AsSingle()
                .NonLazy();
        }
    }
}