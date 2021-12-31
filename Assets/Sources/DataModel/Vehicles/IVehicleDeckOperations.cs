using System;
using System.Collections.Generic;

namespace DataModel.Vehicles
{
	public interface IVehicleDeckOperations
	{
		IObservable<IEnumerable<Vehicle>> ObserveLayoutCompletedChanging();
		void ChangeLayout(IReadOnlyList<Vehicle> layout);
	}
}