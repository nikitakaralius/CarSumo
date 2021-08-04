using System.Collections.Generic;
using DataModel.Vehicles;

namespace DataModel.GameData.Vehicles
{
    public interface IVehicleLayoutBuilder
    {
        IVehicleLayout Create(IEnumerable<VehicleId> vehicles);
    }
}