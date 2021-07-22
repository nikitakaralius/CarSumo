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
        }

        private void BindPlayerSelect()
        {
            Container
                .Bind<IPlayerSelect>()
                .To<PlayerSelect>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerProfileBuilder()
        {
            Container
                .Bind<IPlayerProfileBinder>()
                .To<AddressablesPlayerProfileBinder>()
                .AsSingle();
        }
    }
}