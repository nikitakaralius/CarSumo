using UnityEngine;

namespace CarSumo.Camera
{
    public class CameraRotator : MonoBehaviour
    {
        [SerializeField] private Transform _rotationArm;

        [Header("Preferences")] 
        [SerializeField] private float _defaultSpeed;

        public void Rotate(float angle)
        {
            var angleToRotate = angle * _defaultSpeed * Time.deltaTime;
            transform.Rotate(transform.up, angleToRotate);
        }
    }
}

