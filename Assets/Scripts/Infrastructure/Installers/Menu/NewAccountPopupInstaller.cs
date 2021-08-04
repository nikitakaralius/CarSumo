using Menu.Accounts;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Menu
{
    public class NewAccountPopupInstaller : MonoInstaller
    {
        [SerializeField] private NewAccountPopupView _accountPopup;

        public override void InstallBindings()
        {
            BindNewAccountIconInterfaces();
            BindAccountPopup();
        }

        private void BindAccountPopup()
        {
            Container
                .Bind<IAccountPopup>()
                .FromInstance(_accountPopup)
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