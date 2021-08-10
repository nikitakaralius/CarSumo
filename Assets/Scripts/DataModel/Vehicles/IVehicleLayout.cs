using UniRx;

namespace DataModel.Vehicles
{
    public interface IVehicleLayout : IVehicleLayoutOperations
    {
        IReadOnlyReactiveCollection<VehicleId> ActiveVehicles { get; }
    }
}