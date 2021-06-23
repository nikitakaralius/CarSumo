namespace CarSumo.Vehicles.Stats
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

        public abstract VehicleStats GetStatsInternal();
    }
}
