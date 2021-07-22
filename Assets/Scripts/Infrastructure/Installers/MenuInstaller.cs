using CarSumo.Infrastructure.Factories.Menu;
using CarSumo.Infrastructure.Services.Instantiate;
using CarSumo.Menu.Models;
using CarSumo.Players.Models;
using Zenject;

namespace CarSumo.Infrastructure.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayerProfileBuilder();
            BindPlayerProfilesProvider();
            BindPlayerViewSelect();
            BindPlayersSelectAndPlayersUpdater();
            BindInstantiateService();
        }
        
        private void BindInstantiateService()
        {
            Container
                .Bind<IInstantiateService>()
                .FromInstance(new ZenjectAddressableInstantiateService(Container))
                .NonLazy();
        }


        private void BindPlayersSelectAndPlayersUpdater()
        {
            Container
                .BindInterfacesTo<PlayerSelect>()
                .AsSingle();
        }

        private void BindPlayerProfilesProvider()
        {
            Container
                .Bind<IPlayerProfilesProvider>()
                .FromFactory<PlayerProfilesProviderFactory>()
                .AsSingle();
        }

        private void BindPlayerViewSelect()
        {
            Container
                .Bind<IPlayerViewSelect>()
                .To<PlayerViewSelect>()
                .AsSingle();
        }

        private void BindPlayerProfileBuilder()
        {
            Container
                .Bind<IPlayerProfileBuilder>()
                .To<AddressablesPlayerProfileBuilder>()
                .AsSingle();
        }
    }
}