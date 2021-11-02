using System.Threading;
using System.Threading.Tasks;
using AI.StateMachine.Common;
using AI.Structures;
using CarSumo.Vehicles;

namespace AI.StateMachine.States
{
	public class AIDriveOnTargetState : IAsyncState
	{
		private readonly IVehiclePairProvider _provider;

		public AIDriveOnTargetState(IVehiclePairProvider provider)
		{
			_provider = provider;
		}
		
		private Vehicle ControlledVehicle => _provider.Value.Controlled;

		public async Task DoAsync(CancellationToken token)
		{
			ControlledVehicle.Engine.SpeedUp(1);
			
			await Task.CompletedTask;
		}
	}
}