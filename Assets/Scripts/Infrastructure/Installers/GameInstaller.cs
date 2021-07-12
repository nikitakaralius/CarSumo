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