using CarSumo.Vehicles.Speedometers;

namespace CarSumo.Vehicles
{
    public interface IVehicleEngine
    {
        void TurnOn(IVehicleSpeedometer speedometer);
    }
}
