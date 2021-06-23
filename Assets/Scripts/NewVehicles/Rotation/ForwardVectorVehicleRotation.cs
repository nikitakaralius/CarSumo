using UnityEngine;
using CarSumo.NewVehicles.Stats;

namespace CarSumo.NewVehicles.Rotation
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

        public void RotateBy(Vector3 vector)
        {
            var rotationalSpeed = _statsProvider.GetStats().RotationalSpeed;
            _transform.forward = Vector3.MoveTowards(_transform.forward, vector, rotationalSpeed);
        }
    }
}
