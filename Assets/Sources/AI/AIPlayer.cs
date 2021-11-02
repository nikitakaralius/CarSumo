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
		[SerializeField] private int _thinkMillisecondsDelay;
		[SerializeField] private float _prepareDuration;
		
		private const Team BotTeam = Team.Blue;
		private const Team EnemyTeam = Team.Red;
		
		[Inject]
		private void Construct(ITeamChange teamChange, IVehicleTracker tracker, ITeamPresenter teamPresenter, IAsyncTimeOperationPerformer performer)
		{
			AIStateMachine stateMachine = new AIStateMachine(new IAIState[]
			{
				new AISelectTargetState(tracker, BotTeam, EnemyTeam),
				new AITestState()
			});

			teamPresenter.ActiveTeam.Subscribe(team =>
			{
				if (team == BotTeam)
					stateMachine.Enter<AISelectTargetState>();

			});
		}
	}
}