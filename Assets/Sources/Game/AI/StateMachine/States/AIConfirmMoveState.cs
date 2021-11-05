using AI.StateMachine.Common;
using AI.StateMachine.Messaging;
using AI.Structures;
using CarSumo.Teams.TeamChanging;
using CarSumo.Vehicles;
using Sirenix.Utilities;

namespace AI.StateMachine.States
{
	public class AIConfirmMoveState : IAIState, ITickable, ITransferReceiver<VehiclePair>
	{
		private readonly ITeamChange _teamChange;
		private VehiclePair _package;

		public AIConfirmMoveState(ITeamChange teamChange)
		{
			_teamChange = teamChange;
		}

		private Vehicle ControlledVehicle => _package.Controlled;
		
		public void Enter(AIStateMachine stateMachine)
		{
			if (ControlledVehicle.SafeIsUnityNull() == false)
				ControlledVehicle.Engine.TurnOff();
		}

		public void Tick(AIStateMachine stateMachine, float deltaTime)
		{
			if (ControlledVehicle.SafeIsUnityNull())
				Confirm(stateMachine);
			
			if (ControlledVehicle.Engine.Stopped == false)
				return;

			Confirm(stateMachine);
		}

		private void Confirm(AIStateMachine stateMachine)
		{
			_teamChange.ChangeOnNextTeam();
			stateMachine.Enter<IAIState.None>();
		}

		public void Accept(VehiclePair package)
		{
			_package = package;
		}
	}
}