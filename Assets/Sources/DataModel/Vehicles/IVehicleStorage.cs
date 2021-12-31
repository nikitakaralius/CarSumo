using UniRx;

namespace DataModel.Vehicles
{
    public interface IVehicleStorage
    {
        IReadOnlyReactiveCollection<Vehicle> BoughtVehicles { get; }
    }
}