using System.Collections.Generic;
using System.Linq;
using AI.Extensions;
using AI.StateMachine.Common;
using AI.Structures;
using CarSumo.Teams;
using CarSumo.Units.Tracking;
using CarSumo.Vehicles;

namespace AI.StateMachine.States
{
	public class AISelectTargetState : IAIState
	{
		private readonly IVehicleTracker _tracker;

		private readonly Team _aiTeam;
		private readonly Team _enemyTeam;

		public AISelectTargetState(IVehicleTracker tracker, Team aiTeam, Team enemyTeam)
		{
			_tracker = tracker;
			_aiTeam = aiTeam;
			_enemyTeam = enemyTeam;
		}

		private IEnumerable<Vehicle> Controlled => _tracker.VehiclesBy(_aiTeam);

		private IEnumerable<Vehicle> Enemy => _tracker.VehiclesBy(_enemyTeam);

		public void Enter(AIStateMachine stateMachine)
		{
			VehiclePair closest = Pairs(Controlled, Enemy)
				.Closest();
			
			stateMachine.Transmit(closest);
			stateMachine.Enter<AIPrepareState>();
		}

		private IEnumerable<VehiclePair> Pairs(IEnumerable<Vehicle> controlledVehicles, IEnumerable<Vehicle> enemyVehicles) =>
			controlledVehicles.SelectMany(controlled => enemyVehicles,
				(controlled, enemy) => new VehiclePair(controlled, enemy));
	}
}