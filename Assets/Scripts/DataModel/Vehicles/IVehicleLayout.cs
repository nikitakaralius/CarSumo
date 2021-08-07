using UniRx;

namespace DataModel.Vehicles
{
    public interface IVehicleLayout
    {
        IReadOnlyReactiveCollection<VehicleId> ActiveVehicles { get; }
        void ChangeActiveVehicle(VehicleId vehicle, int slot);
    }
}