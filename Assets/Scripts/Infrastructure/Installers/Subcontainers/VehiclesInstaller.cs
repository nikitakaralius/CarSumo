using DataModel.GameData.Vehicles;
using Infrastructure.Initialization;
using Infrastructure.Installers.Factories;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class VehiclesInstaller : Installer<VehiclesInstaller>
    {
        public override void InstallBindings()
        {
            BindVehicleLayoutBuilder();
            BindVehicleStorageInitialization();
        }

        private void BindVehicleStorageInitialization()
        {
            Container
                .Bind<VehicleStorageInitialization>()
                .FromNew()
                .AsSingle();
        }

        private void BindVehicleLayoutBuilder()
        {
            Container
                .Bind<IVehicleLayoutBuilder>()
                .FromFactory<VehicleLayoutBuilderFactory>()
                .AsSingle();
        }
    }
}