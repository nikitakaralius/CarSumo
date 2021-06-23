using CarSumo.Teams;

namespace CarSumo.Vehicles.Stats
{
    public struct VehicleStats
    {
        public Team Team { get; }
        public float EnginePower { get; }
        public float RotationalSpeed { get; }

        public VehicleStats(Team team, float enginePower, float rotationalSpeed)
        {
            Team = team;
            EnginePower = enginePower;
            RotationalSpeed = rotationalSpeed;
        }
    }
}
