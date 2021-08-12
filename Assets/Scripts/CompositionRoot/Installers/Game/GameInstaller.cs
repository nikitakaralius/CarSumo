using CarSumo.Coroutines;
using Infrastructure.Installers.Factories;
using Infrastructure.Installers.SubContainers;
using Services.Timer;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;

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
                .FromFactory<CountdownTimer, CountdownTimerFactory>()
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