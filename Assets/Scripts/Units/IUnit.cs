using CarSumo.Teams;
using CarSumo.Vehicles;

namespace CarSumo.Units
{
	public interface IUnit
	{
		Team Team { get; }
		void InitializeVehicleBySelf(Vehicle vehicle);
	}
}