using UniRx;
using UnityEngine;

namespace DataModel.Vehicles
{
    public interface IVehicleStorage
    {
        IReadOnlyReactiveCollection<VehicleId> BoughtVehicles { get; }

        GameObject GetVehicleById(VehicleId id);
    }
}