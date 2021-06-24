using UnityEngine;

namespace CarSumo.Vehicles.Selector
{
    [CreateAssetMenu(fileName = "Vehicle Selector Data", menuName = "CarSumo/Vehicles/Selector/Data")]
    public class VehicleSelectorData : ScriptableObject
    {
        [SerializeField] private float _minSelectDistance = 100.0f;
        [SerializeField] private float _maxSelectDistance = 300.0f;
        [SerializeField] private float _cancelDistancePercent = 1.5f;
        [SerializeField] private float _maxAccelerationMultiplier = 4.0f;
        
        public float MinSelectDistance => _minSelectDistance;
        public float MaxSelectDistance => _maxSelectDistance;
        public float CancelDistancePercent => _cancelDistancePercent;

        public float CalculatePercentage(float swipeDistance)
        {
            var clampedDistance = Mathf.Clamp(swipeDistance, _minSelectDistance, _maxSelectDistance);

            return (clampedDistance - _minSelectDistance) / (_maxSelectDistance - _minSelectDistance) * 100;
        }

        public float CalculateAccelerationMultiplier(float swipeDistance)
        {
            return _maxAccelerationMultiplier * CalculatePercentage(swipeDistance) / 100.0f;
        }
    }
}