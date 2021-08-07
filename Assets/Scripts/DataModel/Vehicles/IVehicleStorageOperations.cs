using System.Collections.Generic;

namespace DataModel.Vehicles
{
    public interface IVehicleStorageOperations
    {
        void ChangeOrder(IReadOnlyList<VehicleId> order);
    }
}