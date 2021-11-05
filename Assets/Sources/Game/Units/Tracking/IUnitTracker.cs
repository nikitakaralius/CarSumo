using System.Collections.Generic;
using CarSumo.Teams;
using CarSumo.Vehicles;
using UniRx;

namespace CarSumo.Units.Tracking
{
	public interface IUnitTracker
	{
		IReadOnlyReactiveProperty<int> GetUnitsAlive(Team team);
	}

	public interface IVehicleTracker
	{
		IEnumerable<Vehicle> VehiclesBy(Team team);
	}
}