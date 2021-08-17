using CarSumo.Teams;

namespace CarSumo.Vehicles.Stats
{
	public class TeamVehicleStats : IVehicleStatsProvider
	{
		private readonly Team _team;
		private readonly IVehicleStatsProvider _wrappedEntity;
		
		public TeamVehicleStats(Team team, IVehicleStatsProvider wrappedEntity)
		{
			_team = team;
			_wrappedEntity = wrappedEntity;
		}
		
		public VehicleStats GetStats()
		{
			VehicleStats wrappedEntityStats = _wrappedEntity.GetStats();
			
			return new VehicleStats(_team,
				wrappedEntityStats.NormalizedDrivingSpeed,
				wrappedEntityStats.DrivingTime,
				wrappedEntityStats.RotationalSpeed);
		}
	}
}