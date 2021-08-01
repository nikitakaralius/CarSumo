using DataModel.GameData.Resources;
using Infrastructure.Initialization;
using Zenject;

namespace Infrastructure.Installers.Bootstrap.SubContainers
{
    public class ResourcesInstaller : Installer<ResourcesInstaller>
    {
        public override void InstallBindings()
        {
            BindInitialResourcesProvider();
            BindResourcesStorageInitialization();
        }

        private void BindResourcesStorageInitialization()
        {
            Container
                .Bind<ResourcesStorageInitialization>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindInitialResourcesProvider()
        {
            Container
                .Bind<IInitialResourceStorageProvider>()
                .To<InitialGameResourceStorage>()
                .AsSingle();
        }
    }
}