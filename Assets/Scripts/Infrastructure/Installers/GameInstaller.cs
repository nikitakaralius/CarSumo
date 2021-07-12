using CarSumo.Coroutines;
using CarSumo.Infrastructure.Factories;
using CarSumo.Infrastructure.Services.TeamChangeService;
using CarSumo.Infrastructure.Services.TimerService;
using CarSumo.Input;
using CarSumo.Teams;
using CarSumo.Units;
using CarSumo.Vehicles.Factory;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace CarSumo.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private VehicleHierarchy _vehicleHierarchy;
        [SerializeField] private GameObject _swipeScreenPrefab;
        [SerializeField] private GameObject _axisProviderPrefab;

        public override void InstallBindings()
        {
            BindUnitTracker();
            BindVehicleHierarchy();
            BindTeamDefiner();
            BindSwipeInputScreen();
            BindInputAxisProvider();
            BindTeamChangeService();

            var instance = new CoroutineExecutor(this);
            
            Container
                .Bind<CoroutineExecutor>()
                .FromInstance(instance)
                .AsSingle();

            Container
                .Bind<ITimerService>()
                .FromFactory<CountdownTimerFactory>()
                .AsSingle();
        }

        private void BindTeamChangeService()
        {
            Container
                .Bind<ITeamChangeService>()
                .FromFactory<TeamChangeServiceFactory>()
                .AsSingle();
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

        private void BindTeamDefiner()
        {
            Container
                .Bind<ITeamDefiner>()
                .To<SequentialTeamDefiner>()
                .AsSingle();
        }

        private void BindVehicleHierarchy()
        {
            Container
                .Bind<IVehicleHierarchy>()
                .FromInstance(_vehicleHierarchy)
                .AsSingle();
        }

        private void BindUnitTracker()
        {
            var instance = new UnitTracker(2);

            Container
                .Bind<IUnitTracker>()
                .FromInstance(instance)
                .AsSingle();
        }
    }
}