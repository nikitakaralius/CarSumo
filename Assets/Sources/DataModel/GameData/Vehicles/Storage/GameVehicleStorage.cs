using System;
using System.Collections.Generic;
using System.Linq;
using DataModel.Vehicles;
using UniRx;

namespace DataModel.GameData.Vehicles
{
    public class GameVehicleStorage : IVehicleStorage, IVehicleStorageOperations
    {
        private readonly ReactiveCollection<Vehicle> _boughtVehicles;

        public GameVehicleStorage(IEnumerable<Vehicle> vehicles)
        {
            _boughtVehicles = new ReactiveCollection<Vehicle>(vehicles);
        }
        
        public IReadOnlyReactiveCollection<Vehicle> BoughtVehicles => _boughtVehicles;
        
        public void ChangeOrder(IReadOnlyList<Vehicle> order)
        {
            if (order.Count != _boughtVehicles.Count)
            {
                throw new InvalidOperationException("Trying to change order with different count");
            }

            Vehicle[] cachedVehicles = _boughtVehicles.ToArray();

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

        public void Add(Vehicle vehicle)
        {
	        _boughtVehicles.Add(vehicle);
        }
    }
}