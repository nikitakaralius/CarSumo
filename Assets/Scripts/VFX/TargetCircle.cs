using UnityEngine;
using CarSumo.Utilities;
using CarSumo.VFX.Core;

namespace CarSumo.VFX
{
    public class TargetCircle : MonoEnableEmitter
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _frequency;
        [SerializeField] private float _amplitude;
        [SerializeField] private float _bias;

        private Vector3 _actualSize;

        private void OnEnable() => _actualSize = transform.localScale;

        private void Update()
        {
            ChangeSize();
            Rotate();
        }

        private void ChangeSize()
        {
            var newAxisSize = GetSizeMultiplier() * _actualSize.x;
            transform.localScale = new Vector3(newAxisSize, _actualSize.y, newAxisSize);
        }

        private void Rotate()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * _rotationSpeed);
        }

        private float GetSizeMultiplier()
        {
            return Mathf.Abs(Trigonometry.CosHarmonicMotion(Time.time, _amplitude, _frequency, 0))
                   + _bias;
        }
    }
}