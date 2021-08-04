using Services.Instantiate;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class InstantiationInstaller : Installer<InstantiationInstaller>
    {
        public override void InstallBindings()
        {
            BindInstantiation();
        }

        private void BindInstantiation()
        {
            Container
                .Bind<IAsyncInstantiation>()
                .To<DiInstantiation>()
                .AsSingle()
                .NonLazy();
        }
    }
}