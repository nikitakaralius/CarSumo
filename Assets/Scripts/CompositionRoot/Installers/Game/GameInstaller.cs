using CarSumo.Coroutines;
using Infrastructure.Installers.SubContainers;
using Services.Timer;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private float _timerSecondsToElapse;

        public override void InstallBindings()
        {
            BindMainCamera();
            BindTimer();
            BindCoroutineExecutor();
            ProcessSubContainers();
        }

        private void ProcessSubContainers()
        {
            InstantiationInstaller.Install(Container);
            TeamsInstaller.Install(Container);
            UnitsInstaller.Install(Container);
        }

        private void BindTimer()
        {
            Container
                .BindInterfacesAndSelfTo<CountdownTimer>()
                .FromInstance(new CountdownTimer(_timerSecondsToElapse, new CoroutineExecutor(this)))
                .AsSingle();
        }

        private void BindMainCamera()
        {
            Container
                .Bind<Camera>()
                .FromInstance(_mainCamera)
                .AsSingle();
        }

        private void BindCoroutineExecutor()
        {
            Container
                .Bind<CoroutineExecutor>()
                .FromInstance(new CoroutineExecutor(this))
                .AsSingle();
        }
    }
}