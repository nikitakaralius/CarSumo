using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using CarSumo.Teams;
using GameModes;
using GameModes.Extensions;
using UnityEngine;
using Zenject;

namespace Menu.GameModes.OneDevice
{
	public class OneDeviceGameModeEntry : MonoBehaviour
	{
		private GameStateMachine _stateMachine;
		private IGameModePreferences _gameModePreferences;

		[Inject]
		private void Construct(GameStateMachine stateMachine, IGameModePreferences preferences)
		{
			_stateMachine = stateMachine;
			_gameModePreferences = preferences;
		}

		public void TryEnterGame()
		{
			if (_gameModePreferences.CanEnterGameModeWith(Team.Blue, Team.Red))
			{
				_stateMachine.Enter<GameEntryState>();
			}
		}
	}
}