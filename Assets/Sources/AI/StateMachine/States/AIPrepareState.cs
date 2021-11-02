using AI.StateMachine.Common;
using AI.StateMachine.Messaging;
using AI.Structures;
using CarSumo.Vehicles;
using UnityEngine;

namespace AI.StateMachine.States
{
	public class AIPrepareState : IAIState, ITickable, ITransferReceiver<VehiclePair>
	{
		private const float Duration = 1.2f;
		
		private VehiclePair _package;
		private float _enteredTime;
		private Vector3 _enteredDirection;
		
		public void Enter(AIStateMachine stateMachine)
		{
			_enteredTime = Time.time;
			_enteredDirection = ControlledVehicle.transform.forward;
		}

		private Vector3 DirectionToTarget =>
			Vector3.ProjectOnPlane(_package.Direction * -1, ControlledVehicle.transform.up);

		private Vehicle ControlledVehicle => _package.Controlled;

		public void Tick(AIStateMachine stateMachine, float deltaTime)
		{
			if (_package.Valid == false)
				stateMachine.Enter<AISelectTargetState>();
			
			if (Time.time <= _enteredTime + Duration)
			{
				float percentDone = (Time.time - _enteredTime) / Duration;
				Vector3 direction = Vector3.Lerp(_enteredDirection, DirectionToTarget, percentDone);
				ControlledVehicle.Rotation.RotateBy(direction);
			}
		}

		public void Accept(VehiclePair package)
		{
			_package = package;
		}
	}
}