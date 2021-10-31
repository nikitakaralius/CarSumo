using DataModel.Vehicles;

namespace Menu.Vehicles.Layout
{
	public interface IVehicleLayoutChanger
	{
		void AddVehicleToChange(VehicleId vehicle);
	}
}