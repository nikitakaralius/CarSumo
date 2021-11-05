using AI.StateMachine.Common;
using AI.Structures;
using CarSumo.Vehicles;

namespace AI.StateMachine.States
{
	public class AIDriveOnState : IAIState, ITransferReceiver<VehiclePair>
	{
		private const int EnginePower = 1;
		
		private VehiclePair _package;

		private Vehicle ControlledVehicle => _package.Controlled;
		
		public void Enter(AIStateMachine stateMachine)
		{
			if (_package.Valid == false)
				stateMachine.Enter<AISelectTargetState>();
			
			ControlledVehicle.Engine.SpeedUp(EnginePower);
			stateMachine.Enter<AIConfirmMoveState>();
		}

		public void Accept(VehiclePair package)
		{
			_package = package;
		}
	}
}