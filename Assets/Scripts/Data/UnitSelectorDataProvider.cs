using UnityEngine;

namespace CarSumo.Data
{
    [CreateAssetMenu(fileName = "Unit Selector Data Provider", menuName = "CarSumo/Providers/Unit Selector", order = 0)]
    public class UnitSelectorDataProvider : ScriptableObject
    {
        [SerializeField] private float _minSelectDistance = 100.0f;
        [SerializeField] private float _maxSelectDistanceValue = 400.0f;
        [SerializeField] private float _cancelDistancePercent = 1.5f;
        [SerializeField] private float _maxAccelerationMultiplier = 4.0f;
        
        public float MinSelectDistance => _minSelectDistance;
        public float CancelDistancePercent => _cancelDistancePercent;

        public float CalculatePercentage(float swipeDistance)
        {
            var clampedDistance = Mathf.Clamp(swipeDistance, _minSelectDistance, _maxSelectDistanceValue);

            return (clampedDistance - _minSelectDistance) / (_maxSelectDistanceValue - _minSelectDistance) * 100;
        }

        public float CalculateAccelerationMultiplier(float swipeDistance)
        {
            return _maxAccelerationMultiplier * CalculatePercentage(swipeDistance) / 100.0f;
        }
    }
}