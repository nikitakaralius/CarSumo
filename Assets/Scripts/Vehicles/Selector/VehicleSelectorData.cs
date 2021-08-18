using UnityEngine;

namespace CarSumo.Vehicles.Selector
{
    [CreateAssetMenu(fileName = "Vehicle Selector Data", menuName = "CarSumo/Vehicles/Selector/Data")]
    public class VehicleSelectorData : ScriptableObject
    {        
        public float MinSelectDistance => _minSelectDistance;
        public float MaxSelectDistance => _maxSelectDistance;
        public float CancelDistancePercent => _cancelDistancePercent;
        public float MAXAccelerationTimeMultiplier => _maxAccelerationTimeMultiplier;
        public float TimeForMove => _timeForMove;

        [SerializeField] private float _minSelectDistance = 100.0f;
        [SerializeField] private float _maxSelectDistance = 300.0f;
        [SerializeField] private float _cancelDistancePercent = 1.5f;
        [SerializeField] private float _maxAccelerationTimeMultiplier = 4.0f;
        [SerializeField] private float _timeForMove = 2.0f;
    }
}