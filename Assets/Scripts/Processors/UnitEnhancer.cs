using CarSumo.Level;
using CarSumo.Units;
using CarSumo.Units.Stats;
using UnityEngine;

namespace CarSumo.Processors
{
    public class UnitEnhancer : MonoBehaviour
    {
        [SerializeField] private VehicleDestroyer _destroyer;
        [SerializeField] private VehicleSelector _selector;
        [SerializeField] private bool _allowSuicide = false;

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
            var vehicleToUpgrade = _selector.LastActingVehicle;

            if (_allowSuicide || IsSuicide(destroyingEntity, vehicleToUpgrade))
                return;

            vehicleToUpgrade.SendUpgradeRequest();
        }

        private bool IsSuicide(IVehicleStatsProvider first, IVehicleStatsProvider second)
        {
            return first.GetStats().Team == second.GetStats().Team;
        }
    }
}