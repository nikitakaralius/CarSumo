using System;
using CarSumo.DataModel.Accounts;
using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using CarSumo.Teams;
using CarSumo.Units.Tracking;
using GameModes;
using UniRx;
using Zenject;

namespace Game.Endgame
{
	public class EndGameTracking : IEndGameMessage, IInitializable, IDisposable
	{
		private readonly IUnitTracking _unitTracking;
		private readonly IGameModePreferences _preferences;
		private readonly GameStateMachine _stateMachine;

		private readonly Subject<PersonalizedEndGameStatus> _endObserver = new Subject<PersonalizedEndGameStatus>();
		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

		private IEndgameStatusProvider _provider;
		
		protected EndGameTracking(IUnitTracking unitTracking, IGameModePreferences preferences, GameStateMachine stateMachine)
		{
			_unitTracking = unitTracking;
			_preferences = preferences;
			_stateMachine = stateMachine;
		}

		public IObservable<PersonalizedEndGameStatus> ObserveEnding() => _endObserver;

		public void Initialize()
		{
			_unitTracking
				.UnitsAliveOf(Team.Blue)
				.ObserveCountChanged()
				.Subscribe(count => TryMakeWinner(Team.Red, count))
				.AddTo(_subscriptions);

			_unitTracking
				.UnitsAliveOf(Team.Red)
				.ObserveCountChanged()
				.Subscribe(count => TryMakeWinner(Team.Blue, count))
				.AddTo(_subscriptions);
		}

		public void Dispose() => _subscriptions.Dispose();

		public void Bind(IEndgameStatusProvider provider) => _provider = provider;
		
		private void TryMakeWinner(Team team, int enemiesAlive)
		{
			if (enemiesAlive != 0)
				return;
			
			_stateMachine.Enter<EndGameState>();
			Account winnerAccount = _preferences.GetAccountByTeam(team).Value;
			_endObserver.OnNext(_provider.Status(team, winnerAccount));
		}
	}
}