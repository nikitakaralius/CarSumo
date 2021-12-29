using System;
using System.Collections.Generic;

namespace DataModel.Vehicles
{
	public interface IVehicleDeckOperations
	{
		IObservable<IEnumerable<VehicleId>> ObserveLayoutCompletedChanging();
		void ChangeLayout(IReadOnlyList<VehicleId> layout);
	}
}