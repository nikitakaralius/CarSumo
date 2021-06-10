using CarSumo.Teams;
using UnityEngine;

namespace CarSumo.Units.Stats
{
    [CreateAssetMenu(fileName = "Vehicle Type Stats", menuName = "CarSumo/Vehicles/Type Stats")]
    public class VehicleTypeStats : VehicleStatsDecorator
    {
        [SerializeField] private float _pushForceModifier = 5.0f;
        [SerializeField] private float _rotationSpeed = 25.0f;

        protected override VehicleStats GetStatsInternal()
        {
            return new VehicleStats
            {
                PushForceModifier = _pushForceModifier,
                RotationSpeed = _rotationSpeed,
                Team = WrappedEntity.GetStats().Team
            };
        }
    }
}