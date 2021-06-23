using Sirenix.OdinInspector;

namespace CarSumo.Vehicles.Stats
{
    public abstract class VehicleStatsDecoratorSO : SerializedScriptableObject, IVehicleStatsProvider
    {
        public VehicleStats GetStats()
        {
            return GetStatsInternal();
        }

        public abstract VehicleStats GetStatsInternal();
    }
}
