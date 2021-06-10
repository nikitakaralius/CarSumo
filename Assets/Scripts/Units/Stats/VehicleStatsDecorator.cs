using UnityEngine;

namespace CarSumo.Units.Stats
{
    public abstract class VehicleStatsDecorator : ScriptableObject, IVehicleStatsProvider
    {
        protected IVehicleStatsProvider WrappedEntity;

        public IVehicleStatsProvider Init(IVehicleStatsProvider wrappedEntity)
        {
            WrappedEntity = wrappedEntity;
            return this;
        }

        public VehicleStats GetStats()
        {
            return GetStatsInternal();
        }

        protected abstract VehicleStats GetStatsInternal();
    }
}