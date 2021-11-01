using System.Threading.Tasks;
using AI.StateMachine.Common;
using AI.Structures;
using CarSumo.Vehicles;

namespace AI.StateMachine.States
{
	public class AIDriveOnTargetState : IAsyncState
	{
		private readonly PairTransfer _transfer;

		public AIDriveOnTargetState(PairTransfer transfer)
		{
			_transfer = transfer;
		}
		
		private Vehicle ControlledVehicle => _transfer.Pair.Controlled;

		public async Task DoAsync()
		{
			ControlledVehicle.Engine.SpeedUp(1);
			
			await Task.CompletedTask;
		}
	}
}