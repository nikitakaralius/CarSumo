using UnityEngine;

namespace CarSumo.NewVehicles
{
    public class VehicleEngine : MonoBehaviour, IVehicleEngine
    {
        private IVehicleSpeedometer _speedometer;

        public void TurnOn(IVehicleSpeedometer speedometer)
        {
            _speedometer = speedometer;
        }
    }
}
