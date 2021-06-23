using Sirenix.OdinInspector;

namespace CarSumo.NewVehicles.Stats
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
