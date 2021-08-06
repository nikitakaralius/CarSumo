using DataModel.GameData.Vehicles;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class VehicleAssetsProviderInstaller : MonoInstaller
    {
        [SerializeField] private VehicleAssetsProviderSo _assetsProvider;

        public override void InstallBindings()
        {
            BindVehicleAssetsProvider();
        }

        private void BindVehicleAssetsProvider()
        {
            Container
                .Bind<IVehicleAssetsProvider>()
                .FromInstance(_assetsProvider)
                .AsSingle();
        }
    }
}