using System.Threading.Tasks;
using AI;
using CarSumo.Cameras;
using CarSumo.Teams;
using CarSumo.Vehicles.Selector;
using Game.Endgame;
using Game.Level;
using Game.Rules;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Mediation
{
	public interface IMediator
	{
		Task BootAsync();
		void DeployAI();
		void RememberTeamCameraPosition(Team? targetTeam, bool remember);
		void ChooseRules<TPickerRules>() where TPickerRules : VehiclePicker.IRules;
		void ConfigureEndgame<TStatusProvider>() where TStatusProvider : IEndgameStatusProvider, new();
	}
	
	public class GameMediator : MonoBehaviour, IMediator
	{
		[Inject] private readonly RulesRepository _rulesRepository;
		[Inject] private readonly EndGameTracking _endGameTracking;
		
		[SerializeField, Required, SceneObjectsOnly] private GameBoot _boot;
		[SerializeField, Required, SceneObjectsOnly] private AIPlayer _aiPlayer;
		[SerializeField, Required, SceneObjectsOnly] private CameraTeamTransposer _camera;
		[SerializeField, Required, SceneObjectsOnly] private VehicleSelector _selector;
		
		[Button, DisableInEditorMode] public async Task BootAsync() => await _boot.BootAsync();
		[Button, DisableInEditorMode] public void DeployAI() => _aiPlayer.Enable();
		[Button, DisableInEditorMode] public void RememberTeamCameraPosition(Team? targetTeam, bool remember) => _camera.RememberPosition(targetTeam, remember);
		public void ChooseRules<TPickerRules>() where TPickerRules : VehiclePicker.IRules => _selector.Initialize(_rulesRepository.InstanceOf<TPickerRules>());
		public void ConfigureEndgame<TStatusProvider>() where TStatusProvider : IEndgameStatusProvider, new() => 
			_endGameTracking.Bind(new TStatusProvider());
	}
}