using UniRx;

namespace DataModel.Vehicles
{
    public interface IVehicleDeck : IVehicleDeckOperations
    {
        IReadOnlyReactiveCollection<Vehicle> ActiveVehicles { get; }
    }
}