using System.Threading.Tasks;
using AI.StateMachine.Common;
using AI.StateMachine.States;
using BaseData.CompositeRoot.Common;
using BaseData.Timers;
using CarSumo.Teams;
using CarSumo.Teams.TeamChanging;
using CarSumo.Units.Tracking;
using UniRx;
using UnityEngine;
using Zenject;

namespace AI
{
	public class AIPlayer : CompositionRoot
	{
		private const Team BotTeam = Team.Red;
		private const Team EnemyTeam = Team.Blue;
		
		private AIStateMachine _stateMachine;
		private ITeamPresenter _teamPresenter;
		
		[Inject]
		private void Construct(ITeamChange teamChange, IVehicleTracker tracker, ITeamPresenter teamPresenter)
		{
			_stateMachine = new AIStateMachine(new IAIState[]
			{
				new IAIState.None(),
				new AISelectTargetState(tracker, BotTeam, EnemyTeam),
				new AIPrepareState(new UnityTimer(), 1.2f),
				new AIDriveOnState(),
				new AIConfirmMoveState(teamChange)
			});

			_teamPresenter = teamPresenter;
		}

		public override Task ComposeAsync()
		{
			_teamPresenter.ActiveTeam.Subscribe(team =>
			{
				if (team == BotTeam)
					_stateMachine.Enter<AISelectTargetState>();
			});

			return Task.CompletedTask;
		}

		private void Update()
		{
			_stateMachine.Tick(Time.deltaTime);
		}
	}
}