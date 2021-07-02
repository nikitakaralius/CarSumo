using CarSumo.Teams;
using CarSumo.Vehicles.Stats;

namespace CarSumo.Vehicles
{
    public interface IVehicle : IVehicleStatsProvider
    {
        Team Team { get; }
        void Upgrade();
        void Destroy();

        public class FakeVehicle : IVehicle
        {
            public FakeVehicle(Team team)
            {
                Team = team;
            }

            public Team Team { get; }

            public void Destroy()
            {
            }

            public VehicleStats GetStats()
            {
                return new VehicleStats(Team, 0.0f, 0.0f);
            }

            public void Upgrade()
            {
            }
        }
    }
}
