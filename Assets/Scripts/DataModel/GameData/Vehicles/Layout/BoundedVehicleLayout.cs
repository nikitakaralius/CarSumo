using System;
using System.Collections.Generic;
using System.Linq;
using DataModel.Vehicles;
using Sirenix.Utilities;
using UniRx;

namespace DataModel.GameData.Vehicles
{
    public class BoundedVehicleLayout : IVehicleLayout
    {
        private readonly ReactiveCollection<VehicleId> _activeVehicles;

        public BoundedVehicleLayout(int slotsAmount, IEnumerable<VehicleId> vehicles)
        {
            if (vehicles.Count() > slotsAmount)
            {
                throw new InvalidOperationException("Amount of vehicles can not be greater than slots");
            }
            
            _activeVehicles = new ReactiveCollection<VehicleId>(vehicles);
            _activeVehicles.SetLength(slotsAmount);
        }
        
        public IReadOnlyReactiveCollection<VehicleId> ActiveVehicles => _activeVehicles;
        
        public void ReplaceActiveVehicle(VehicleId vehicle, int slot)
        {
            _activeVehicles[slot] = vehicle;
        }
    }
}