using CarSumo.Level;
using CarSumo.Teams;
using CarSumo.Units;
using CarSumo.Units.Stats;
using UnityEngine;

namespace CarSumo.Processors
{
    public class UnitEnhancer : MonoBehaviour
    {
        [SerializeField] private VehicleDestroyer _destroyer;
        [SerializeField] private VehicleSelector _selector;

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
            var upgradeTeam = new SequentialTeamDefiner().DefineTeam(destroyingEntity.GetStats().Team);
            var vehicleToUpgrade = _selector.LastActingVehicles[(int)upgradeTeam];

            if (vehicleToUpgrade is null)
                return;

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