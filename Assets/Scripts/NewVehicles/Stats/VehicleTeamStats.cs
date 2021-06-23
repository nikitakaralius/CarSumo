using CarSumo.Teams;

namespace CarSumo.NewVehicles.Stats
{
    public class VehicleTeamStats : VehicleStatsDecorator
    {
        private readonly Team _team;

        public VehicleTeamStats(IVehicleStatsProvider wrappedEntity, Team team) : base(wrappedEntity)
        {
            _team = team;
        }

        public override VehicleStats GetStatsInternal()
        {
            var baseStats = WrappedEntity.GetStats();

            return new VehicleStats(_team, baseStats.EnginePower, baseStats.RotationalSpeed);
        }
    }
}
