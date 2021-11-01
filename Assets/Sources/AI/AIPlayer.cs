using AI.StateMachine.Common;
using AI.StateMachine.States;
using AI.Structures;
using CarSumo.Teams;
using CarSumo.Teams.TeamChanging;
using CarSumo.Units.Tracking;
using UniRx;
using UnityEngine;
using Zenject;

namespace Sources.AI
{
	public class AIPlayer : MonoBehaviour
	{
		[SerializeField] private int _thinkMillisecondsDelay;
		
		private const Team BotTeam = Team.Blue;
		private const Team EnemyTeam = Team.Red;

		private AIStateMachine _stateMachine;

		[Inject]
		private void Construct(ITeamChange teamChange, IVehicleTracker tracker, ITeamPresenter teamPresenter)
		{
			var transfer = new PairTransfer();

			_stateMachine = new AIStateMachine(new IAsyncState[]
			{
				new AIThinkDelayState(_thinkMillisecondsDelay),
				new AISelectTargetState(tracker, transfer, BotTeam, EnemyTeam),
				new AIPrepareState(transfer),
				new AIThinkDelayState(_thinkMillisecondsDelay),
				new AIDriveOnTargetState(transfer),
				new AICompleteMoveState(teamChange, transfer)
			});

			teamPresenter.ActiveTeam.Subscribe(team =>
			{
				if (team == BotTeam)
					_stateMachine.RunAsync();
			});
		}
	}
}