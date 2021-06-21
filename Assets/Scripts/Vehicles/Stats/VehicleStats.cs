using CarSumo.Teams;

namespace CarSumo.Vehicles.Stats
{
    public struct VehicleStats
    {
        public Team Team { get; set; }
        public float PushForceModifier { get; set; }
        public float RotationSpeed { get; set; }
    }
}