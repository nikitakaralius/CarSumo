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

        public IVehicleLayout Create(IEnumerable<VehicleId> vehicles)
        {
            return new BoundedVehicleLayout(_slotsAmount, vehicles);
        }
    }
}