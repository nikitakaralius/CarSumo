using UnityEngine;

namespace CarSumo.Data
{
    [CreateAssetMenu(fileName = "Unit Data", menuName = "CarSumo/Units/Data")]
    public class UnitData : ScriptableObject
    {
        [SerializeField] private float _rotationSpeed = 25.0f;
        [SerializeField] private float _pushForce = 5.0f;

        public float RotationSpeed => _rotationSpeed;
        public float PushForce => _pushForce;
    }
}