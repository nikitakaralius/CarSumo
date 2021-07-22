using CarSumo.Infrastructure.Factories.Menu;
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