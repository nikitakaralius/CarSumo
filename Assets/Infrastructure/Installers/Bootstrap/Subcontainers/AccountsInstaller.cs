using System;
using CarSumo.DataModel.GameData.Accounts;
using Zenject;

namespace Infrastructure.Installers.Bootstrap.SubContainers
{
    public class AccountsInstaller : Installer<AccountsInstaller>
    {
        public override void InstallBindings()
        {
            BindAccountBinding();
            BindAccountSerialization();
            BindAccountStorageInterfaces();
            BindAccountStorageSave();
        }

        private void BindAccountStorageSave()
        {
            throw new NotImplementedException("Register save on initialization");
        }

        private void BindAccountStorageInterfaces()
        {
            throw new NotImplementedException("Register storage on initialization");
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