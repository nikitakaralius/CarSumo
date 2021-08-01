using CarSumo.DataModel.GameData.Accounts;
using Infrastructure.Initialization;
using Zenject;

namespace Infrastructure.Installers.Bootstrap.SubContainers
{
    public class AccountsInstaller : Installer<AccountsInstaller>
    {
        public override void InstallBindings()
        {
            BindAccountBinding();
            BindAccountSerialization();
            BindAccountStorageInitialization();
        }

        private void BindAccountStorageInitialization()
        {
            Container
                .Bind<AccountStorageInitialization>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindAccountSerialization()
        {
            Container
                .Bind<IAccountSerialization>()
                .To<AccountSerialization>()
                .AsSingle();
        }

        private void BindAccountBinding()
        {
            Container
                .Bind<IAsyncAccountBinding>()
                .To<AddressableAccountBinding>()
                .AsSingle();
        }
    }
}