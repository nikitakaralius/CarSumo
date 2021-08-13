using AdvancedAudioSystem;
using CarSumo.Audio;
using CarSumo.DataModel.Accounts;
using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using CarSumo.Teams;
using CarSumo.Units.Tracking;
using GameModes;
using UI.Accounts;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI
{
	public class WinStateHandler : MonoBehaviour
	{
		[SerializeField] private GameObject _winnerWindow;
		[SerializeField] private WinnerAccount _winnerAccount;
		[SerializeField] private AudioCue _winCue;
		
		private IUnitTracker _unitTracker;
		private IGameModePreferences _gameModePreferences;
		private BackgroundMusic _backgroundMusic;
		private GameStateMachine _stateMachine;

		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

		[Inject]
		private void Construct(IUnitTracker unitTracker, IGameModePreferences preferences, BackgroundMusic music, GameStateMachine stateMachine)
		{
			_unitTracker = unitTracker;
			_gameModePreferences = preferences;
			_stateMachine = stateMachine;
			_backgroundMusic = music;
		}

		private void OnEnable()
		{
			_unitTracker
				.GetUnitsAlive(Team.Blue)
				.Subscribe(unitsAlive => TryOpenWinWindow(Team.Blue, unitsAlive))
				.AddTo(_subscriptions);
			
			_unitTracker
				.GetUnitsAlive(Team.Red)
				.Subscribe(unitsAlive => TryOpenWinWindow(Team.Red, unitsAlive))
				.AddTo(_subscriptions);
		}

		private void OnDisable()
		{
			_subscriptions?.Dispose();
		}

		private void TryOpenWinWindow(Team team, int unitsAlive)
		{
			if (unitsAlive == 0)
			{
				_stateMachine.Enter<PauseState>();
				
				_backgroundMusic.ChangeAudioCue(_winCue);
				
				Account winnerAccount = _gameModePreferences.GetAccountByTeam(team).Value;
				
				_winnerWindow.SetActive(true);
				_winnerAccount.ChangeAccount(winnerAccount);
			}
		}
	}
}