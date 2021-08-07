using DataModel.GameData.Vehicles;
using Infrastructure.Installers.Factories;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class VehiclesInstaller : Installer<VehiclesInstaller>
    {
        public override void InstallBindings()
        {
            BindVehicleLayoutBuilder();
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