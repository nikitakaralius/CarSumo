using CarSumo.Teams;
using CarSumo.Vehicles;

namespace CarSumo.Units
{
	public interface IUnit
	{
		Team Team { get; }
		Vehicle Vehicle { get; }
		void InitializeVehicleBySelf(Vehicle vehicle);
	}
}