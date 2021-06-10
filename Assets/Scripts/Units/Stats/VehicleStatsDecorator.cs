namespace CarSumo.Units.Stats
{
    public abstract class VehicleStatsDecorator : IVehicleStatsProvider
    {
        protected readonly IVehicleStatsProvider WrappedEntity;

        protected VehicleStatsDecorator(IVehicleStatsProvider wrappedEntity)
        {
            WrappedEntity = wrappedEntity;
        }

        public VehicleStats GetStats()
        {
            return GetStatsInternal();
        }

        protected abstract VehicleStats GetStatsInternal();
    }
}