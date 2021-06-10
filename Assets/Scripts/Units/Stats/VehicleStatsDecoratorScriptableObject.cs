using UnityEngine;

namespace CarSumo.Units.Stats
{
    public abstract class VehicleStatsDecoratorScriptableObject : ScriptableObject, IVehicleStatsProvider
    {
        public abstract VehicleTypeStats Init();

        public VehicleStats GetStats()
        {
            return GetStatsInternal();
        }

        protected abstract VehicleStats GetStatsInternal();
    }
}