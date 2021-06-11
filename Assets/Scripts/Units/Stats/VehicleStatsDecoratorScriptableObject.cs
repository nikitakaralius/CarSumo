using Sirenix.OdinInspector;

namespace CarSumo.Units.Stats
{
    public abstract class VehicleStatsDecoratorScriptableObject : SerializedScriptableObject, IVehicleStatsProvider
    {
        public abstract VehicleTypeStats Init();

        public VehicleStats GetStats()
        {
            return GetStatsInternal();
        }

        protected abstract VehicleStats GetStatsInternal();
    }
}