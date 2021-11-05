using System.Threading.Tasks;
using AI.StateMachine.Common;
using AI.StateMachine.States;
using AI.Structures;
using BaseData.Timers;
using CarSumo.Teams.TeamChanging;
using CarSumo.Units.Tracking;
using UniRx;
using UnityEngine;
using Zenject;

namespace AI
{
	public class AIPlayer : MonoBehaviour
	{
		[SerializeField] private AIPreferences _preferences;
		
		private AIStateMachine _stateMachine;
		private ITeamPresenter _teamPresenter;
		
		[Inject]
		private void Construct(ITeamChange teamChange, IVehicleTracker tracker, ITeamPresenter teamPresenter, ITimer timer)
		{
			_stateMachine = new AIStateMachine(new IAIState[]
			{
				new IAIState.None(),
				new AISelectTargetState(tracker, _preferences.BotTeam, _preferences.EnemyTeam),
				new AIPrepareState(timer, _preferences.PrepareDuration),
				new AIDriveOnState(),
				new AIConfirmMoveState(teamChange)
			});

			_teamPresenter = teamPresenter;
		}

		public Task ComposeAsync()
		{
			_teamPresenter.ActiveTeam.Subscribe(team =>
			{
				if (team == _preferences.BotTeam)
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