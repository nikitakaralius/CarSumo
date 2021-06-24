using TMPro;
using UnityEngine;
using CarSumo.Vehicles.Speedometers;
using CarSumo.VFX.Core;

namespace CarSumo.Vehicles.Engine
{
    public class SpeedometerText : MonoEnableEmitter
    {
        [SerializeField] private TMP_Text _enginePercentage;

        private IVehicleSpeedometer _speedometer;

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
