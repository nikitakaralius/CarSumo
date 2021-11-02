using AI.StateMachine.Common;
using AI.Structures;
using CarSumo.Teams.TeamChanging;
using CarSumo.Vehicles;
using Sirenix.Utilities;

namespace AI.StateMachine.States
{
	public class AIConfirmMoveState : IAIState, ITransferReceiver<VehiclePair>
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
			
			_teamChange.ChangeOnNextTeam();
		}

		public void Accept(VehiclePair package)
		{
			_package = package;
		}
	}
}