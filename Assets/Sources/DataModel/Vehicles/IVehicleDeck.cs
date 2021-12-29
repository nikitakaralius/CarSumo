using UniRx;

namespace DataModel.Vehicles
{
    public interface IVehicleDeck : IVehicleDeckOperations
    {
        IReadOnlyReactiveCollection<VehicleId> ActiveVehicles { get; }
    }
}