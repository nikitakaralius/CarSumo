using CarSumo.DataModel.GameResources;
using UniRx;

namespace DataModel.Vehicles
{
    public interface IClientVehicleOperations
    {
        bool TryAcquire(IResourceStorage resourceStorage);
    }
}