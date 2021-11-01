using System.Threading;
using System.Threading.Tasks;
using AI.StateMachine.Common;
using AI.Structures;
using CarSumo.Vehicles;
using UnityEngine;

namespace AI.StateMachine.States
{
	public class AIPrepareState : IAsyncState
	{
		private readonly PairTransfer _transfer;

		public AIPrepareState(PairTransfer transfer)
		{
			_transfer = transfer;
		}
		
		private Vector3 TargetDirection => _transfer.Pair.Direction * -1;

		private Vehicle ControlledVehicle => _transfer.Pair.Controlled;
		
		public async Task DoAsync(CancellationToken token)
		{
			await Task.WhenAll(
				Rotate(token),
				ConfigureBoost(token));
		}

		private async Task Rotate(CancellationToken token)
		{
			Vector3 direction = ControlledVehicle.transform.forward;

			while (token.IsCancellationRequested == false && direction != TargetDirection)
			{
				direction = Vector3.MoveTowards(direction, TargetDirection, Time.deltaTime);

				ControlledVehicle.Rotation.RotateBy(direction);

				await Task.Yield();
			}
		}

		private async Task ConfigureBoost(CancellationToken token)
		{
			var vehicleSpeedometer = new AISpeedometer();
			ControlledVehicle.Engine.TurnOn(vehicleSpeedometer);

			const float time = 2.0f;
			float enteredTime = Time.time;

			while (token.IsCancellationRequested == false && Time.time <= time + enteredTime)
			{
				float percentDone = (Time.time - enteredTime) / time * 100.0f;
				
				vehicleSpeedometer.PowerPercentage = percentDone;
				
				await Task.Yield();
			}
		}
	}
}