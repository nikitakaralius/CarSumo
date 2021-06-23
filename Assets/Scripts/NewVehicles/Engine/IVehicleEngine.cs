using CarSumo.NewVehicles.Speedometers;

namespace CarSumo.NewVehicles
{
    public interface IVehicleEngine
    {
        void TurnOn(IVehicleSpeedometer speedometer);
    }
}
