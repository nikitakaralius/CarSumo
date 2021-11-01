using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Extensions;
using AI.StateMachine.Common;
using AI.Structures;
using CarSumo.Teams;
using CarSumo.Units.Tracking;
using CarSumo.Vehicles;

namespace AI.StateMachine.States
{
	public class AISelectTargetState : IAsyncState
	{
		private readonly IVehicleTracker _tracker;

		private readonly Team _aiTeam;
		private readonly Team _enemyTeam;

		private readonly PairTransfer _transfer;
		
		public AISelectTargetState(IVehicleTracker tracker, PairTransfer transfer, Team aiTeam, Team enemyTeam)
		{
			_tracker = tracker;
			_transfer = transfer;
			_aiTeam = aiTeam;
			_enemyTeam = enemyTeam;
		}

		private IEnumerable<Vehicle> ControlledVehicles => _tracker.VehiclesBy(_aiTeam);

		private IEnumerable<Vehicle> EnemyVehicles => _tracker.VehiclesBy(_enemyTeam);

		public async Task DoAsync()
		{
			VehiclePair closestPair = Pairs(ControlledVehicles, EnemyVehicles)
				.Closest();

			_transfer.Pair = closestPair;
			
			await Task.CompletedTask;
		}

		private IEnumerable<VehiclePair> Pairs(IEnumerable<Vehicle> controlled, IEnumerable<Vehicle> enemy) =>
			controlled.SelectMany(
				controlledVehicle => enemy,
				(controlledVehicle, enemyVehicle) =>
					new VehiclePair(controlledVehicle, enemyVehicle));
	}
}