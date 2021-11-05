using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using CarSumo.Teams;
using Game.GameModes.Composites;
using GameModes;
using GameModes.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Menu.GameModes.OneDevice
{
	public class MenuGameEntry : SerializedMonoBehaviour
	{
		[SerializeField] private IGameComposite _composite;
		
		private GameStateMachine _stateMachine;
		private GameModeRegistry _registry;

		[Inject]
		private void Construct(GameStateMachine stateMachine, GameModeRegistry registry)
		{
			_stateMachine = stateMachine;
			_registry = registry;
		}

		public void TryEnterGame()
		{
			if (_registry.CanEnterGameModeWith(Team.Blue, Team.Red))
			{
				_registry.ChooseGameComposite(_composite);
				_stateMachine.Enter<GameEntryState>();
			}
		}
	}
}