namespace DataModel.Vehicles
{
	public interface IVehicleLayoutOperations
	{
		bool TryChangeActiveVehicle(VehicleId vehicle, int slot);
	}
}