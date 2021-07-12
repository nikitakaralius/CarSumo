using CarSumo.Level;
using CarSumo.Teams;
using CarSumo.Vehicles.Selector;
using CarSumo.Vehicles.Stats;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CarSumo.Units
{
    public class UnitEnhancer : SerializedMonoBehaviour
    {
        [SerializeField] private VehicleDestroyer _destroyer;
        [SerializeField] private VehicleSelector _selector;

        private IPreviousTeamDefiner _upgradeTeamDefiner;

        [Inject]
        private void Construct(IPreviousTeamDefiner previousTeamDefiner)
        {
            _upgradeTeamDefiner = previousTeamDefiner;
        }

        private void OnEnable()
        {
            _destroyer.VehicleDestroying += OnVehicleDestroying;
        }

        private void OnDisable()
        {
            _destroyer.VehicleDestroying -= OnVehicleDestroying;
        }

        private void OnVehicleDestroying(IVehicleStatsProvider destroyingEntity)
        {
            var entityTeam = destroyingEntity.GetStats().Team;
            var teamToUpgrade = _upgradeTeamDefiner.DefineTeam(entityTeam);

            var vehicleToUpgrade = _selector.LastValidVehicles[teamToUpgrade];

            if (IsSuicide(destroyingEntity, vehicleToUpgrade))
                return;

            vehicleToUpgrade.Upgrade();
        }

        private bool IsSuicide(IVehicleStatsProvider first, IVehicleStatsProvider second)
        {
            return first.GetStats().Team == second.GetStats().Team;
        }
    }
}