using UnityEngine;
using CarSumo.Vehicles.Stats;

namespace CarSumo.Vehicles.Rotation
{
    public class ForwardVectorVehicleRotation : IRotation
    {
        private readonly Transform _transform;
        private readonly VehicleStats _stats;

        public ForwardVectorVehicleRotation(Transform transform, VehicleStats stats)
        {
            _transform = transform;
            _stats = stats;
        }

        public void RotateBy(Vector3 vector)
        {
            var rotationalSpeed = _stats.RotationalSpeed;
            _transform.forward = Vector3.MoveTowards(_transform.forward, vector, rotationalSpeed);
        }
    }
}
