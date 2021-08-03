using Services.Instantiate;
using Services.SceneManagement;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class ServicesInstaller : Installer<ServicesInstaller>
    {
        public override void InstallBindings()
        {
            BindAsyncInstantiation();
            BindSceneLoadingInterfaces();
        }

        private void BindSceneLoadingInterfaces()
        {
            Container
                .BindInterfacesAndSelfTo<AddressableSceneLoading>()
                .AsSingle()
                .NonLazy();
        }

        private void BindAsyncInstantiation()
        {
            Container
                .Bind<IAsyncInstantiation>()
                .To<DiInstantiation>()
                .AsSingle()
                .NonLazy();
        }
    }
}