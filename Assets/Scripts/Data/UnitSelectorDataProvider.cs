using UnityEngine;

namespace CarSumo.Data
{
    [CreateAssetMenu(fileName = "Unit Selector Data Provider", menuName = "CarSumo/Providers/Unit Selector", order = 0)]
    public class UnitSelectorDataProvider : ScriptableObject
    {
        [SerializeField] private float _minSelectDistance = 100.0f;
        [SerializeField] private float _distanceDivider = 100.0f;
        [SerializeField] private float _minDistanceValue = 0.0f;
        [SerializeField] private float _maxDistanceValue = 10.0f;

        public float MinSelectDistance => _minSelectDistance;

        public float CalculateMultiplier(float distance)
            => Mathf.Clamp(distance / _distanceDivider, _minDistanceValue, _maxDistanceValue);
    }
}