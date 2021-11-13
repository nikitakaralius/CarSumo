using System.Threading.Tasks;
using CarSumo.Units;
using Services.Timer.InGameTimer;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Level
{
	public class GameBoot : MonoBehaviour
	{
		[SerializeField, Required, SceneObjectsOnly] private UnitInitializing _unitInitializing;
		[SerializeField, Required, SceneObjectsOnly] private Image _blackScreen;

		private IConfiguredTimerOperations _timer;

		[Inject]
		private void Construct(IConfiguredTimerOperations timer)
		{
			_timer = timer;
		}
		
		public async Task BootAsync()
		{
			await _unitInitializing.InitializeAsync();
			_timer.Start();
			_blackScreen.CrossFadeAlpha(0, 1, false);
		}
	}
}