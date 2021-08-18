using BaseData.Abstract;
using CarSumo.Vehicles.Rotation;
using CarSumo.Vehicles.Stats;

namespace CarSumo.Vehicles
{
    public interface IVehicle : IDestroyable
    {
		IVehicleEngine Engine { get; }
		IRotation Rotation { get; }
		VehicleStats Stats { get; }
    }
}
