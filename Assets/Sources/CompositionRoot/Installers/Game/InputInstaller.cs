using CarSumo.Cameras;
using CarSumo.Input;
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
            BindInputCameraInput();
            BindSwipeInputScreen();
        }

        private void BindInputCameraInput()
        {
            Container
                .Bind<CameraInput>()
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