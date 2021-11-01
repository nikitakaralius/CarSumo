using System.Threading;
using System.Threading.Tasks;
using AI.StateMachine.Common;
using AI.Structures;
using CarSumo.Teams.TeamChanging;
using CarSumo.Vehicles;

namespace AI.StateMachine.States
{
	public class AICompleteMoveState : IAsyncState
	{
		private const int DelayBeforeChecking = 500;

		private readonly ITeamChange _teamChange;
		private readonly PairTransfer _transfer;

		private readonly CancellationToken _token;
		
		public AICompleteMoveState(ITeamChange teamChange, PairTransfer transfer, CancellationToken token)
		{
			_teamChange = teamChange;
			_transfer = transfer;
			_token = token;
		}

		private Vehicle ControlledVehicle => _transfer.Pair.Controlled;

		private bool IsMoving => 
			_token.IsCancellationRequested == false
			&& ControlledVehicle.Engine.Stopped == false;

		public async Task DoAsync()
		{
			await Task.Delay(DelayBeforeChecking, _token);
			
			while (IsMoving)
			{
				await Task.Yield();
			}
			
			if (_token.IsCancellationRequested)
				return;

			ControlledVehicle.Engine.TurnOff();
			_teamChange.ChangeOnNextTeam();
		}
	}
}