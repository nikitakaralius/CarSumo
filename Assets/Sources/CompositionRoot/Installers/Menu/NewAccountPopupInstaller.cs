using Menu.Accounts;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Menu
{
    public class NewAccountPopupInstaller : MonoInstaller
    {
        [SerializeField] private AccountRegistryPopup _accountRegistryPopup;

        public override void InstallBindings()
        {
            BindNewAccountIconInterfaces();
            BindAccountPopup();
        }

        private void BindAccountPopup()
        {
            Container
                .BindInterfacesAndSelfTo<AccountRegistryPopup>()
                .FromInstance(_accountRegistryPopup)
                .AsSingle();
        }

        private void BindNewAccountIconInterfaces()
        {
            Container
                .BindInterfacesAndSelfTo<NewAccountIcon>()
                .AsSingle();
        }
    }
}