using UniRx;

namespace DataModel.Vehicles
{
    public interface IVehicleStorage
    {
        IReadOnlyReactiveCollection<VehicleId> BoughtVehicles { get; }
    }
}