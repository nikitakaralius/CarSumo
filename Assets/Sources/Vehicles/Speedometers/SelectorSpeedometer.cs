using CarSumo.Input;
using CarSumo.Vehicles.Selector;
using UnityEngine;

namespace CarSumo.Vehicles.Speedometers
{
    public class SelectorSpeedometer : IVehicleSpeedometer
    {
        public float PowerPercentage { get; private set; }

        private readonly VehicleSelectorData _data;

        public SelectorSpeedometer(VehicleSelectorData data)
        {
            _data = data;
        }

        public void CalculatePowerBySwipeData(Swipe swipe)
        {
            PowerPercentage = CalculatePowerByDistance(swipe.Distance);
        }

        private float CalculatePowerByDistance(float distance)
        {
            var clampedDistance = Mathf.Clamp(distance, _data.MinSelectDistance, _data.MaxSelectDistance);

            return (clampedDistance - _data.MinSelectDistance) / (_data.MaxSelectDistance - _data.MinSelectDistance) * 100;
        }
    }
}
