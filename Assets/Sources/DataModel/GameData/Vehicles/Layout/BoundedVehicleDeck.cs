using System;
using System.Collections.Generic;
using System.Linq;
using DataModel.Vehicles;
using Sirenix.Utilities;
using UniRx;

namespace DataModel.GameData.Vehicles
{
    public class BoundedVehicleDeck : IVehicleDeck
    {
        private readonly ReactiveCollection<Vehicle> _activeVehicles;
        private readonly Subject<IEnumerable<Vehicle>> _layoutCompletedChanging;

        public BoundedVehicleDeck(int slotsAmount, IEnumerable<Vehicle> vehicles)
        {
            if (vehicles.Count() > slotsAmount)
            {
                throw new InvalidOperationException("Amount of vehicles can not be greater than slots");
            }
            
            _activeVehicles = new ReactiveCollection<Vehicle>(vehicles);
            _activeVehicles.SetLength(slotsAmount);

            _layoutCompletedChanging = new Subject<IEnumerable<Vehicle>>();
        }
        
        public IReadOnlyReactiveCollection<Vehicle> ActiveVehicles => _activeVehicles;

        public IObservable<IEnumerable<Vehicle>> ObserveLayoutCompletedChanging()
        {
	        return _layoutCompletedChanging;
        }

        public void ChangeLayout(IReadOnlyList<Vehicle> layout)
        {
	        if (layout.Count != _activeVehicles.Count)
	        {
		        throw new InvalidOperationException("Trying to change layout with different size");
	        }

	        for (var i = 0; i < _activeVehicles.Count; i++)
	        {
		        _activeVehicles[i] = layout[i];
	        }
	        
	        _layoutCompletedChanging.OnNext(_activeVehicles);
        }
    }
}