using DataModel.Vehicles;

namespace Sources.Cards
{
	public interface IVehicleCardsRepository
	{
		VehicleCard StatsOf(VehicleId vehicle);
	}
}