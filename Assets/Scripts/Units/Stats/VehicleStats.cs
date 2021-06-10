using CarSumo.Teams;

namespace CarSumo.Units.Stats
{
    public struct VehicleStats
    {
        public Team Team { get; set; }
        public float PushForceModifier { get; set; }
        public float RotationSpeed { get; set; }
    }
}