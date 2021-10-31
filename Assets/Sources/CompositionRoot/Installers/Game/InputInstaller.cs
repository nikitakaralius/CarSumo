using CarSumo.Input;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Game
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _swipeScreenPrefab;
        [SerializeField] private GameObject _axisProviderPrefab;

        public override void InstallBindings()
        {
            BindInputAxisProvider();
            BindSwipeInputScreen();
        }

        private void BindInputAxisProvider()
        {
            Container
                .Bind<AxisState.IInputAxisProvider>()
                .FromComponentInNewPrefab(_axisProviderPrefab)
                .AsSingle();
        }

        private void BindSwipeInputScreen()
        {
            Container
                .Bind<ISwipeScreen>()
                .FromComponentInNewPrefab(_swipeScreenPrefab)
                .AsSingle();
        }
    }
}