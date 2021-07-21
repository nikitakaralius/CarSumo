using CarSumo.Players.Models;
using Zenject;

namespace CarSumo.Infrastructure.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayerProfileBuilder();
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