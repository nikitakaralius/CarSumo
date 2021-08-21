using System;
using System.Collections.Generic;
using System.Linq;
using DataModel.Vehicles;
using UniRx;

namespace DataModel.GameData.Vehicles
{
    public class GameVehicleStorage : IVehicleStorage, IVehicleStorageOperations
    {
        private readonly ReactiveCollection<VehicleId> _boughtVehicles;

        public GameVehicleStorage(IEnumerable<VehicleId> vehicles)
        {
            _boughtVehicles = new ReactiveCollection<VehicleId>(vehicles);
        }
        
        public IReadOnlyReactiveCollection<VehicleId> BoughtVehicles => _boughtVehicles;
        
        public void ChangeOrder(IReadOnlyList<VehicleId> order)
        {
            if (order.Count != _boughtVehicles.Count)
            {
                throw new InvalidOperationException("Trying to change order with different count");
            }

            VehicleId[] cachedVehicles = _boughtVehicles.ToArray();

            for (var i = 0; i < _boughtVehicles.Count; i++)
            {
                if (cachedVehicles.Any(vehicle => vehicle == order[i]))
                {
                    _boughtVehicles[i] = order[i];
                }
                else
                {
                    throw new InvalidOperationException("Trying to change order with non-existing account");
                }
            }
        }

        public void Add(VehicleId vehicle)
        {
	        _boughtVehicles.Add(vehicle);
        }
    }
}