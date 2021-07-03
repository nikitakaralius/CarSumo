using UnityEngine;

namespace CarSumo
{
    [System.Serializable]
    public struct Range
    {
        public float Min => _min;
        public float Max => _max;

        [SerializeField] private float _min;
        [SerializeField] private float _max;
    }
}
