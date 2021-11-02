using System.Threading;
using System.Threading.Tasks;
using AI.StateMachine.Common;
using AI.Structures;
using CarSumo.Vehicles;
using Sources.BaseData.Operations;
using UnityEngine;

namespace AI.StateMachine.States
{
	public class AIPrepareState : IAsyncState
	{
		private readonly IVehiclePairProvider _provider;
		private readonly IAsyncTimeOperationPerformer _performer;
		private readonly float _duration;

		public AIPrepareState(IVehiclePairProvider provider, IAsyncTimeOperationPerformer performer, float duration)
		{
			_provider = provider;
			_performer = performer;
			_duration = duration;
		}

		private Vector3 TargetDirection =>
			Vector3.ProjectOnPlane(_provider.Value.Direction * -1, ControlledVehicle.transform.up);

		private Vehicle ControlledVehicle => _provider.Value.Controlled;

		public async Task DoAsync(CancellationToken token)
		{
			var vehicleSpeedometer = new AISpeedometer();
			ControlledVehicle.Engine.TurnOn(vehicleSpeedometer);

			await Task.WhenAll(
				_performer.DoUntilTimeElapsedAsync(token, _duration,
					percent => Rotate(percent, ControlledVehicle.transform.forward)),
				_performer.DoUntilTimeElapsedAsync(token, _duration,
					percent => ConfigureBoost(percent, vehicleSpeedometer)));
		}

		private void Rotate(float percentDone, Vector3 direction)
		{
			direction = Vector3.Lerp(direction, TargetDirection, percentDone);
			ControlledVehicle.Rotation.RotateBy(direction);
		}

		private void ConfigureBoost(float percentDone, AISpeedometer speedometer)
		{
			speedometer.PowerPercentage = percentDone * 100.0f;
		}
	}
}