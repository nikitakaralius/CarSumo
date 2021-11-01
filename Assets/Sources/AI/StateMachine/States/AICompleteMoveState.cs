using System.Threading.Tasks;
using AI.StateMachine.Common;
using CarSumo.Teams.TeamChanging;

namespace AI.StateMachine.States
{
	public class AICompleteMoveState : IAsyncState
	{
		private readonly ITeamChange _teamChange;

		public AICompleteMoveState(ITeamChange teamChange)
		{
			_teamChange = teamChange;
		}

		public async Task DoAsync()
		{
			_teamChange.ChangeOnNextTeam();
			
			await Task.CompletedTask;
		}
	}
}