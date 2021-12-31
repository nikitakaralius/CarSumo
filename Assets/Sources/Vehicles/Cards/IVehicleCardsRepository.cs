using DataModel.Vehicles;

namespace Sources.Cards
{
	public interface IVehicleCardsRepository
	{
		VehicleCard StatsOf(Vehicle vehicle);
	}
}