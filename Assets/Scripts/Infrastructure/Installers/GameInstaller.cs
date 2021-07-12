using CarSumo.Teams;
using CarSumo.Units;
using CarSumo.Vehicles.Factory;
using UnityEngine;
using Zenject;

namespace CarSumo.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private VehicleHierarchy _vehicleHierarchy;
        
        public override void InstallBindings()
        {
            BindUnitTracker();
            BindVehicleHierarchy();

            Container.Bind<ITeamDefiner>().To<SequentialTeamDefiner>().AsSingle();
            Container.Bind<IPreviousTeamDefiner>().To<PreviousSequentialTeamDefiner>().AsSingle();
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