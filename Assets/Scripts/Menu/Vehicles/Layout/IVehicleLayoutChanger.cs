using DataModel.Vehicles;

namespace Menu.Vehicles.Layout
{
	public interface IVehicleLayoutChanger
	{
		bool TryChangeVehicleOn(VehicleId vehicle);
	}
}