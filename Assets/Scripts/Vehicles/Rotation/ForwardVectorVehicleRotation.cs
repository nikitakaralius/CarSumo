using UnityEngine;
using CarSumo.Vehicles.Stats;

namespace CarSumo.Vehicles.Rotation
{
    public class ForwardVectorVehicleRotation : IRotation
    {
        private readonly Transform _transform;
        private readonly IVehicleStatsProvider _statsProvider;

        public ForwardVectorVehicleRotation(Transform transform, IVehicleStatsProvider statsProvider)
        {
            _transform = transform;
            _statsProvider = statsProvider;
        }

        private VehicleStats Stats => _statsProvider.GetStats();

        public void RotateBy(Vector3 vector)
        {
            var rotationalSpeed = Stats.RotationalSpeed;
            _transform.forward = Vector3.MoveTowards(_transform.forward, vector, rotationalSpeed);
        }
    }
}
