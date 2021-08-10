using UniRx;

namespace DataModel.Vehicles
{
    public interface IVehicleLayout
    {
        IReadOnlyReactiveCollection<VehicleId> ActiveVehicles { get; }
        bool TryChangeActiveVehicle(VehicleId vehicle, int slot);
    }
}