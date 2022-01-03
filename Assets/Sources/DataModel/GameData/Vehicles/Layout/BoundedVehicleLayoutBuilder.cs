using System.Collections.Generic;
using DataModel.Vehicles;

namespace DataModel.GameData.Vehicles
{
    public class BoundedVehicleLayoutBuilder : IVehicleLayoutBuilder
    {
        private readonly int _slotsAmount;

        public BoundedVehicleLayoutBuilder(int slotsAmount)
        {
            _slotsAmount = slotsAmount;
        }

        public IVehicleDeck Create(IEnumerable<Vehicle> vehicles)
        {
            return new BoundedVehicleDeck(_slotsAmount, vehicles);
        }
    }
}