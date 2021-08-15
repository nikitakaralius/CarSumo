using System;
using CarSumo.DataModel.Accounts;
using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using CarSumo.Teams;
using CarSumo.Units.Tracking;
using GameModes;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game
{
	public class WinTracker : IWinMessage, IInitializable, IDisposable
	{
		private readonly IUnitTracker _unitTracker;
		private readonly IGameModePreferences _gameModePreferences;
		private readonly GameStateMachine _stateMachine;

		private readonly Subject<Account> _winObserver = new Subject<Account>();
		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();
		
		public WinTracker(IUnitTracker unitTracker, IGameModePreferences gameModePreferences, GameStateMachine stateMachine)
		{
			_unitTracker = unitTracker;
			_gameModePreferences = gameModePreferences;
			_stateMachine = stateMachine;
		}

		public void Initialize()
		{
			_unitTracker
				.GetUnitsAlive(Team.Blue)
				.Subscribe(unitsAlive => TryMakeTeamWin(Team.Red, unitsAlive))
				.AddTo(_subscriptions);
			
			_unitTracker
				.GetUnitsAlive(Team.Red)
				.Subscribe(unitsAlive => TryMakeTeamWin(Team.Blue, unitsAlive))
				.AddTo(_subscriptions);
		}

		public void Dispose()
		{
			_subscriptions.Dispose();
		}

		public IObservable<Account> ObserveWin()
		{
			return _winObserver;
		}

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