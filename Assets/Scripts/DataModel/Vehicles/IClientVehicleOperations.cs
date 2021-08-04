using CarSumo.DataModel.GameResources;
using UniRx;

namespace DataModel.Vehicles
{
    public interface IClientVehicleOperations
    {
        bool TryAcquire(ResourceId resource, IReactiveProperty<int> balance);
        void FitIntoLayout(IVehicleLayout layout);
    }
}