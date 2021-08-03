using DataModel.GameData.Resources;
using DataModel.GameData.Resources.Binding;
using Infrastructure.Initialization;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class ResourcesInstaller : Installer<ResourcesInstaller>
    {
        public override void InstallBindings()
        {
            BindInitialResourcesProvider();
            BindResourcesStorageInitialization();
            BindResourceStorageBinding();
        }

        private void BindResourceStorageBinding()
        {
            Container
                .Bind<IResourceStorageBinding>()
                .To<ResourceStorageBinding>()
                .AsSingle();
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