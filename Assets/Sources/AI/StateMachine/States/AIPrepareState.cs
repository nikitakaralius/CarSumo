using System.Threading;
using System.Threading.Tasks;
using AI.StateMachine.Common;
using AI.Structures;
using CarSumo.Vehicles;
using CarSumo.Vehicles.Speedometers;
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
			Vector3 direction = ControlledVehicle.transform.forward;
			AISpeedometer vehicleSpeedometer = new AISpeedometer();
			
			ControlledVehicle.Engine.TurnOn(vehicleSpeedometer);
			
			
			while (token.IsCancellationRequested == false && direction != TargetDirection)
			{
				direction = Vector3.MoveTowards(direction, TargetDirection, Time.deltaTime);

				vehicleSpeedometer.PowerPercentage =
					Mathf.Lerp(vehicleSpeedometer.PowerPercentage, 100, Time.deltaTime);
				
				ControlledVehicle.Rotation.RotateBy(direction);
				
				await Task.Yield();
			}
		}
	}

	public class AISpeedometer : IVehicleSpeedometer
	{
		public float PowerPercentage { get; set; }
	}
}