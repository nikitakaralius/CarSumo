using BaseData.Timers;
using CarSumo.Coroutines;
using Game;
using Game.Mediation;
using Game.Rules;
using Infrastructure.Installers.Factories;
using Infrastructure.Installers.SubContainers;
using Services.Timer.InGameTimer;
using Sirenix.OdinInspector;
using Game.Endgame;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField, Required, SceneObjectsOnly] private Camera _mainCamera;
        [SerializeField, Required, SceneObjectsOnly] private GameMediator _mediator;
        
        public override void InstallBindings()
        {
            BindMainCamera();
            BindGameMediator();
            BindGameTimer();
            BindTimer();
            BindCoroutineExecutor();
            BindWinTrackerInterfaces();
            BindRulesRepository();
            ProcessSubContainers();
        }

        private void ProcessSubContainers()
        {
            SignalBusInstaller.Install(Container);
            InstantiationInstaller.Install(Container);
            TeamsInstaller.Install(Container);
            UnitsInstaller.Install(Container);
            TimersInstaller.Install(Container);
        }

        private void BindGameTimer() =>
            Container
                .BindInterfacesAndSelfTo<CountdownTimer>()
                .FromFactory<CountdownTimer, CountdownTimerFactory>()
                .AsSingle();

        private void BindTimer() =>
            Container
                .BindInterfacesTo<UnityTimer>()
                .AsSingle();

        private void BindMainCamera() =>
            Container
                .Bind<Camera>()
                .FromInstance(_mainCamera)
                .AsSingle();

        private void BindCoroutineExecutor() =>
            Container
                .Bind<CoroutineExecutor>()
                .FromInstance(new CoroutineExecutor(this))
                .AsSingle();

        private void BindWinTrackerInterfaces() =>
            Container
                .BindInterfacesAndSelfTo<OneDeviceEndGameTracker>()
                .AsSingle();

        private void BindGameMediator() =>
            Container
                .BindInterfacesTo<GameMediator>()
                .FromInstance(_mediator)
                .AsSingle();

        private void BindRulesRepository() =>
            Container
                .Bind<RulesRepository>()
                .AsSingle();
    }
}