using CarSumo.Vehicles.Speedometers;
using CarSumo.VFX.Core;
using TMPro;
using UnityEngine;
using Zenject;

namespace CarSumo.Vehicles.Engine
{
    public class SpeedometerText : MonoEnableEmitter
    {
        [SerializeField] private TMP_Text _enginePercentage;

        private Camera _camera;
        private IVehicleSpeedometer _speedometer;

        [Inject]
        private void Construct(Camera camera)
        {
            _camera = camera;
        }

        public SpeedometerText Init(IVehicleSpeedometer speedometer)
        {
            _speedometer = speedometer;
            return this;
        }

        private void Update()
        {
            _enginePercentage.text = $"{(int)_speedometer.PowerPercentage}%";
            transform.forward = Camera.main.transform.forward;
        }
    }
}
