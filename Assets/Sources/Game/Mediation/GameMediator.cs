using System.Threading.Tasks;
using AI;
using CarSumo.Units;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Mediation
{
	public interface IMediator
	{
		Task BootAsync();
		void DeployAI();
	}
	
	public class GameMediator : MonoBehaviour, IMediator
	{
		[SerializeField, Required, SceneObjectsOnly] private UnitInitializing _unitInitializing;
		[SerializeField, Required, SceneObjectsOnly] private AIPlayer _aiPlayer;

		[Button, DisableInEditorMode] public async Task BootAsync() => await _unitInitializing.InitializeAsync();
		[Button, DisableInEditorMode] public void DeployAI() => _aiPlayer.Enable();
	}
}