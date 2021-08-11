using System.Collections.Generic;

namespace DataModel.Vehicles
{
	public interface IVehicleLayoutOperations
	{
		void ChangeLayout(IReadOnlyList<VehicleId> layout);
	}
}