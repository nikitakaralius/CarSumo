using System.Collections.Generic;
using DataModel.Vehicles;

namespace DataModel.GameData.Vehicles
{
    public interface IVehicleLayoutFactory
    {
        IVehicleLayout Create(IEnumerable<VehicleId> vehicles);
    }
}