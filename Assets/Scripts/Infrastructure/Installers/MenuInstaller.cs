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
            BindPlayerSelect();
            BindProfilesUpdate();
            BindPlayerProfilesProvider();
            BindPlayerViewSelect();
        }

        private void BindPlayerProfilesProvider()
        {
            Container
                .Bind<IPlayerProfilesProvider>()
                .FromFactory<PlayerProfilesProviderFactory>()
                .AsSingle();
        }

        private void BindProfilesUpdate()
        {
            Container
                .Bind<IPlayerProfilesUpdate>()
                .FromFactory<PlayerProfilesUpdateFactory>()
                .AsSingle();
        }

        private void BindPlayerViewSelect()
        {
            Container
                .Bind<IPlayerViewSelect>()
                .To<PlayerViewSelect>()
                .AsSingle();
        }

        private void BindPlayerSelect()
        {
            Container
                .Bind<IPlayerSelect>()
                .To<PlayerSelect>()
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

    public class PlayerProfilesUpdateFactory : IFactory<IPlayerProfilesUpdate>
    {
        private readonly IPlayerSelect _playerUpdate;

        public PlayerProfilesUpdateFactory(IPlayerSelect playerUpdate)
        {
            _playerUpdate = playerUpdate;
        }

        public IPlayerProfilesUpdate Create()
        {
            return (PlayerSelect)_playerUpdate;
        }
    }
}