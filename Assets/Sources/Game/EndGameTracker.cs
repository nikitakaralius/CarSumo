using System;
using CarSumo.DataModel.Accounts;
using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using CarSumo.Teams;
using CarSumo.Units.Tracking;
using GameModes;
using UniRx;
using Zenject;

namespace Game
{
	public class EndGameTracker : IEndGameMessage, IInitializable, IDisposable
	{
		private readonly IUnitTracking _unitTracking;
		private readonly IGameModePreferences _gameModePreferences;
		private readonly GameStateMachine _stateMachine;

		private readonly Subject<Account> _winObserver = new Subject<Account>();
		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();
		
		public EndGameTracker(IUnitTracking unitTracking, IGameModePreferences gameModePreferences, GameStateMachine stateMachine)
		{
			_unitTracking = unitTracking;
			_gameModePreferences = gameModePreferences;
			_stateMachine = stateMachine;
		}

		public void Initialize()
		{
			_unitTracking
				.UnitsAlive(Team.Blue)
				.ObserveCountChanged()
				.Subscribe(unitsAlive => TryMakeTeamWin(Team.Red, unitsAlive))
				.AddTo(_subscriptions);
			
			_unitTracking
				.UnitsAlive(Team.Red)
				.ObserveCountChanged()
				.Subscribe(unitsAlive => TryMakeTeamWin(Team.Blue, unitsAlive))
				.AddTo(_subscriptions);
		}

		public void Dispose() => _subscriptions.Dispose();

		public IObservable<Account> ObserveEnding() => _winObserver;

		private void TryMakeTeamWin(Team winTeam, int enemyUnitsAlive)
		{
			if (enemyUnitsAlive == 0)
			{
				_stateMachine.Enter<WinState>();

				Account winnerAccount = _gameModePreferences.GetAccountByTeam(winTeam).Value;
				
				_winObserver.OnNext(winnerAccount);
			}
		}
	}
}