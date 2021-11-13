using Game.GameModes.Composites;
using Game.Mediation;
using GameModes;
using UnityEngine;
using Zenject;

namespace Game.Level
{
	public class GameModeBuilder : MonoBehaviour
	{
		private IGameModePreferences _preferences;
		private IMediator _mediator;

		[Inject]
		private void Construct(IGameModePreferences preferences, IMediator mediator)
		{
			_mediator = mediator;
			_preferences = preferences;
		}

		private IGameComposite Composite => _preferences.Composite;

		private void OnEnable() => Composite.Compose(_mediator);
	}
}