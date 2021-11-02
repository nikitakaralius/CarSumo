using System.Threading;
using System.Threading.Tasks;
using AI.StateMachine.Common;
using CarSumo.Teams.TeamChanging;
using CarSumo.Vehicles;

namespace AI.StateMachine.States
{
	public class AICompleteMoveState : IAsyncState
	{
		private const int DelayBeforeChecking = 500;

		private readonly ITeamChange _teamChange;
		private readonly IVehiclePairProvider _provider;
		
		public AICompleteMoveState(ITeamChange teamChange, IVehiclePairProvider provider)
		{
			_teamChange = teamChange;
			_provider = provider;
		}

		private Vehicle ControlledVehicle => _provider.Value.Controlled;

		private bool IsMoving => ControlledVehicle.Engine.Stopped == false;

		public async Task DoAsync(CancellationToken token)
		{
			await Task.Delay(DelayBeforeChecking, token);
			
			while (token.IsCancellationRequested == false && IsMoving) 
				await Task.Yield();

			if (token.IsCancellationRequested)
				return;

			ControlledVehicle.Engine.TurnOff();
			_teamChange.ChangeOnNextTeam();
		}
	}
}