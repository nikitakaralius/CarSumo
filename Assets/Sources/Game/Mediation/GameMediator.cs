using System.Threading.Tasks;
using AI;
using CarSumo.Cameras;
using CarSumo.Teams;
using CarSumo.Units;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Mediation
{
	public interface IMediator
	{
		Task BootAsync();
		void DeployAI();
		void RememberTeamCameraPosition(Team? targetTeam, bool remember);
	}
	
	public class GameMediator : MonoBehaviour, IMediator
	{
		[SerializeField, Required, SceneObjectsOnly] private UnitInitializing _unitInitializing;
		[SerializeField, Required, SceneObjectsOnly] private AIPlayer _aiPlayer;
		[SerializeField, Required, SceneObjectsOnly] private CameraTeamTransposer _camera;
		
		[Button, DisableInEditorMode] public async Task BootAsync() => await _unitInitializing.InitializeAsync();
		[Button, DisableInEditorMode] public void DeployAI() => _aiPlayer.Enable();
		[Button, DisableInEditorMode] public void RememberTeamCameraPosition(Team? targetTeam, bool remember) => _camera.RememberPosition(targetTeam, remember);
	}
}