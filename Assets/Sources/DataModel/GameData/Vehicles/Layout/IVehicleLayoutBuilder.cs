using System.Collections.Generic;
using DataModel.Vehicles;

namespace DataModel.GameData.Vehicles
{
    public interface IVehicleLayoutBuilder
    {
        IVehicleDeck Create(IEnumerable<Vehicle> vehicles);
    }
}