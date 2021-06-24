using CarSumo.Level;
using CarSumo.Teams;
using CarSumo.Vehicles.Selector;
using CarSumo.Vehicles.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarSumo.Processors
{
    public class UnitEnhancer : SerializedMonoBehaviour
    {
        [SerializeField] private VehicleDestroyer _destroyer;
        [SerializeField] private VehicleSelector _selector;
        [SerializeField] private ITeamDefiner _upgradeTeamDefiner = new SequentialTeamDefiner();

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
            var teamToUpgrage = _upgradeTeamDefiner.DefineTeam(entityTeam);

            var vehicleToUpgrade = _selector.LastValidVehicles[teamToUpgrage];

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