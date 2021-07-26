using CarSumo.Infrastructure.Services.Instantiate;
using CarSumo.Vehicles.Factory;
using UnityEngine;
using Zenject;

namespace CarSumo.Infrastructure.Installers
{
    public class VehicleFactoryInstaller : MonoInstaller
    {
        [SerializeField] private VehicleFactory[] _factories;

        public override void InstallBindings()
        {
            Container
                .Bind<IInstantiateService>()
                .To<ZenjectAddressableInstantiateService>()
                .AsTransient()
                .NonLazy();
            
            foreach (VehicleFactory factory in _factories)
            {
                Container.QueueForInject(factory);
            }
        }
    }
}