using UnityEngine;

namespace CarSumo.Units.Stats
{
    [CreateAssetMenu(fileName = "Vehicle Type Stats", menuName = "CarSumo/Vehicles/Type Stats")]
    public class VehicleTypeStats : VehicleStatsDecoratorScriptableObject
    {
        [SerializeField] private float _pushForceModifier = 5.0f;
        [SerializeField] private float _rotationSpeed = 25.0f;

        public override VehicleTypeStats Init()
        {
            return this;
        }

        protected override VehicleStats GetStatsInternal()
        {
            return new VehicleStats
            {
                PushForceModifier = _pushForceModifier,
                RotationSpeed = _rotationSpeed
            };
        }
    }
}