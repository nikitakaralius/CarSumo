using CarSumo.Teams;

namespace CarSumo.Vehicles.Stats
{
    public class VehicleTeamStats : VehicleStatsDecorator
    {
        private readonly Team _team;

        public VehicleTeamStats(IVehicleStatsProvider wrappedEntity, Team team) : base(wrappedEntity)
        {
            _team = team;
        }

        protected override VehicleStats GetStatsInternal()
        {
            return new VehicleStats
            {
                PushForceModifier = WrappedEntity.GetStats().PushForceModifier,
                RotationSpeed =  WrappedEntity.GetStats().RotationSpeed,
                Team = _team
            };
        }
    }
}