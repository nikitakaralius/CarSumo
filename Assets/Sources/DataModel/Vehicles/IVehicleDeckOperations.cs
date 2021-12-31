using System;
using System.Collections.Generic;

namespace DataModel.Vehicles
{
	public interface IVehicleDeckOperations
	{
		IObservable<IVehicleDeck> ObserveLayoutCompletedChanging();
		void ChangeLayout(IReadOnlyList<Vehicle> layout);
	}
}