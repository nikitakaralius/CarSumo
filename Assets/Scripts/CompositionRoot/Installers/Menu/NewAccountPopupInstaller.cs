using Menu.Accounts;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Menu
{
    public class NewAccountPopupInstaller : MonoInstaller
    {
        [SerializeField] private AccountRegistry _accountRegistry;

        public override void InstallBindings()
        {
            BindNewAccountIconInterfaces();
            BindAccountPopup();
        }

        private void BindAccountPopup()
        {
            Container
                .BindInterfacesAndSelfTo<AccountRegistry>()
                .FromInstance(_accountRegistry)
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