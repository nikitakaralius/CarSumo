using AI.StateMachine.Common;
using AI.StateMachine.States;
using CarSumo.Teams;
using CarSumo.Teams.TeamChanging;
using CarSumo.Units.Tracking;
using Sources.BaseData.Operations;
using UniRx;
using UnityEngine;
using Zenject;

namespace AI
{
	public class AIPlayer : MonoBehaviour
	{
		private const Team BotTeam = Team.Blue;
		private const Team EnemyTeam = Team.Red;
		
		private AIStateMachine _stateMachine;

		[Inject]
		private void Construct(ITeamChange teamChange, IVehicleTracker tracker, ITeamPresenter teamPresenter, IAsyncTimeOperationPerformer performer)
		{
			_stateMachine = new AIStateMachine(new IAIState[]
			{
				new AISelectTargetState(tracker, BotTeam, EnemyTeam),
				new AIPrepareState()
			});

			teamPresenter.ActiveTeam.Subscribe(team =>
			{
				if (team == BotTeam)
					_stateMachine.Enter<AISelectTargetState>();
			});
		}

		private void Update()
		{
			_stateMachine.Tick(Time.deltaTime);
		}
	}
}