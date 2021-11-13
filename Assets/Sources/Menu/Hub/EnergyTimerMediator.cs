using CarSumo.DataModel.GameResources;
using DataModel.GameData.GameSave;
using Menu.Resources;
using Services.Timers.Extensions;
using Services.Timers.Realtime;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Hub
{
	public class EnergyTimerMediator : SerializedMonoBehaviour
	{
		private IResourceStorage _storage;
		private ResourceTimers _timers;
		private ResourceTimersSave _save;
		private IClientResourceOperations _resourceOperations;
		
		[Inject]
		private void Construct(ResourceTimers timers,
								ResourceTimersSave save,
								IClientResourceOperations operations,
								IResourceStorage storage)
		{
			_timers = timers;
			_save = save;
			_resourceOperations = operations;
			_storage = storage;
		}

		private void Start() => HandleCallbacks();

		private void HandleCallbacks()
		{
			_timers
				.TimerOf(TimedResource.GameEnergy)
				.Cycles(out IRealtimeTimer timer)
				.Subscribe(cycles =>
				{
					_resourceOperations.Receive(ResourceId.Energy, cycles);
					timer.FlushCycles();
				});
			
			_storage
				.GetResourceAmount(ResourceId.Energy)
				.Subscribe(value =>
				{
					if (value < _storage.GetResourceLimit(ResourceId.Energy).Value)
						timer.Start();
					else
						timer.Stop();
				});
		}

		[Button(Style = ButtonStyle.FoldoutButton), DisableInEditorMode]
		private void LogResourceTimeLeft(TimedResource resource) =>
			Debug.Log(_timers
				.TimerOf(resource)
				.TimeLeft());

		[Button, DisableInEditorMode] private void Save() => _save.Save();
	}
}