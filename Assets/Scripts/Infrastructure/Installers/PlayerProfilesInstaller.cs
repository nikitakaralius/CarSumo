using CarSumo.Menu.Models;
using CarSumo.Players.Models;
using Zenject;

namespace CarSumo.Infrastructure.Installers
{
    public class PlayerProfilesInstaller : Installer<PlayerProfilesInstaller>
    {
        public override void InstallBindings()
        {
            BindPlayerProfileBuilder();
            BindPlayerProfilesProvider();
            BindPlayersSelectAndPlayersUpdater();
        }
        
        private void BindPlayerProfileBuilder()
        {
            Container
                .Bind<IPlayerProfileBuilder>()
                .To<AddressablesPlayerProfileBuilder>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindPlayerProfilesProvider()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerProfilesBinder>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindPlayersSelectAndPlayersUpdater()
        {
            Container
                .BindInterfacesTo<PlayerSelect>()
                .AsSingle()
                .NonLazy();
        }
    }
}