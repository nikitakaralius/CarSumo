using CarSumo.Infrastructure.Services.Instantiate;
using CarSumo.Menu.Models;
using Zenject;

namespace CarSumo.Infrastructure.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayerViewSelect();
            BindInstantiateService();
            PlayerProfilesInstaller.Install(Container);
        }
        
        private void BindInstantiateService()
        {
            Container
                .Bind<IAddressablesInstantiate>()
                .FromInstance(new ZenjectAddressablesInstantiate(Container))
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerViewSelect()
        {
            Container
                .Bind<IPlayerViewSelect>()
                .To<PlayerViewSelect>()
                .AsSingle();
        }
    }
}