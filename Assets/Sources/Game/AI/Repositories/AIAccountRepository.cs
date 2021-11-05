using System.Collections.Generic;
using CarSumo.DataModel.Accounts;
using DataModel.GameData.Vehicles;
using DataModel.Vehicles;

namespace AI.Repositories
{
	public class AIAccountRepository : IAccountRepository
	{
		public IEnumerable<Account> Accounts => new[]
		{
			new Account("John", null, new BoundedVehicleLayout(3, new[]
			{
				VehicleId.Jeep, VehicleId.Jeep, VehicleId.Jeep
			}))
		};
	}
}