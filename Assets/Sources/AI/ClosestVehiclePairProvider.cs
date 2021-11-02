using System.Collections.Generic;
using System.Linq;
using AI.Extensions;
using AI.Structures;
using CarSumo.Teams;
using CarSumo.Units.Tracking;
using CarSumo.Vehicles;

namespace AI
{
	public class ClosestVehiclePairProvider : IVehiclePairProvider
	{
		private readonly IVehicleTracker _tracker;
		
		private readonly Team _aiTeam;
		private readonly Team _enemyTeam;

		public ClosestVehiclePairProvider(IVehicleTracker tracker, Team aiTeam, Team enemyTeam)
		{
			_tracker = tracker;
			_aiTeam = aiTeam;
			_enemyTeam = enemyTeam;
		}

		public VehiclePair Value => Pairs(ControlledVehicles, EnemyVehicles).Closest();
		
		private IEnumerable<Vehicle> ControlledVehicles => _tracker.VehiclesBy(_aiTeam);

		private IEnumerable<Vehicle> EnemyVehicles => _tracker.VehiclesBy(_enemyTeam);
		
		private IEnumerable<VehiclePair> Pairs(IEnumerable<Vehicle> controlled, IEnumerable<Vehicle> enemy) =>
			controlled.SelectMany(
				controlledVehicle => enemy,
				(controlledVehicle, enemyVehicle) =>
					new VehiclePair(controlledVehicle, enemyVehicle));
	}
}