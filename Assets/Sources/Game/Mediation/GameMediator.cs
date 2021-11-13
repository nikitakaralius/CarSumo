using System.Threading.Tasks;
using AI;
using CarSumo.Cameras;
using CarSumo.Teams;
using CarSumo.Units;
using CarSumo.Vehicles.Selector;
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
		void ConfigureSelector<TPickerRules>() where TPickerRules : VehiclePicker.IRules;
	}
	
	public class GameMediator : MonoBehaviour, IMediator
	{
		[Inject] private readonly RulesRepository _rulesRepository;
		
		[SerializeField, Required, SceneObjectsOnly] private UnitInitializing _unitInitializing;
		[SerializeField, Required, SceneObjectsOnly] private AIPlayer _aiPlayer;
		[SerializeField, Required, SceneObjectsOnly] private CameraTeamTransposer _camera;
		[SerializeField, Required, SceneObjectsOnly] private VehicleSelector _selector;
		
		[Button, DisableInEditorMode] public async Task BootAsync() => await _unitInitializing.InitializeAsync();
		[Button, DisableInEditorMode] public void DeployAI() => _aiPlayer.Enable();
		[Button, DisableInEditorMode] public void RememberTeamCameraPosition(Team? targetTeam, bool remember) => _camera.RememberPosition(targetTeam, remember);
		public void ConfigureSelector<TPickerRules>() where TPickerRules : VehiclePicker.IRules => _selector.Initialize(_rulesRepository.InstanceOf<TPickerRules>());
	}
}