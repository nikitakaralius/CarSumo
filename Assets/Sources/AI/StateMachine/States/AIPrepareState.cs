using System.Collections;
using AI.StateMachine.Common;
using AI.StateMachine.Messaging;
using AI.Structures;
using BaseData.Timers;
using CarSumo.Vehicles;
using UnityEngine;

namespace AI.StateMachine.States
{
	public class AIPrepareState : IAIState, ITickable, ITransferReceiver<VehiclePair>
	{
		private const int ModelHasInvertedForwardVector = -1;

		private readonly float _duration;
		private readonly ITimer _timer;
		private readonly AISpeedometer _speedometer = new AISpeedometer();

		private VehiclePair _package;
		private Vector3 _enteredDirection;

		public AIPrepareState(ITimer timer, float duration)
		{
			_timer = timer;
			_duration = duration;
		}

		public void Enter(AIStateMachine stateMachine)
		{
			_timer.Start(_duration);
			_enteredDirection = ControlledVehicle.transform.forward;

			ControlledVehicle.Engine.TurnOn(_speedometer);
		}

		private Vector3 DirectionToTarget =>
			Vector3.ProjectOnPlane(_package.Direction * ModelHasInvertedForwardVector,
				ControlledVehicle.transform.up);

		private Vehicle ControlledVehicle => _package.Controlled;

		public void Tick(AIStateMachine stateMachine, float deltaTime)
		{
			if (_package.Valid == false)
				stateMachine.Enter<AISelectTargetState>();

			RotateTo(DirectionToTarget, _timer);
			ConfigureBoost(_timer);

			if (_timer.Elapsed)
				stateMachine.Enter<AISelectTargetState>();
		}

		private void ConfigureBoost(ITimer timer)
		{
			_speedometer.PowerPercentage = timer.PercentElapsed * 100.0f;
		}

		private void RotateTo(Vector3 target, ITimer timer)
		{
			float percentDone = timer.PercentElapsed;
			Vector3 direction = Vector3.Lerp(_enteredDirection, target, percentDone);
			ControlledVehicle.Rotation.RotateBy(direction);
		}

		public void Accept(VehiclePair package)
		{
			_package = package;
		}
	}
}