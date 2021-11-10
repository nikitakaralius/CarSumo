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
	public class Timers : SerializedMonoBehaviour
	{
		private ResourceTimers _timers;
		private ResourceTimersSave _save;
		private IClientResourceOperations _resourceOperations;
		
		[Inject]
		private void Construct(ResourceTimers timers, ResourceTimersSave save, IClientResourceOperations operations)
		{
			_timers = timers;
			_save = save;
			_resourceOperations = operations;
		}

		private void Start() => BindRewards();

		private void BindRewards() =>
			_timers
				.TimerOf(TimedResource.GameEnergy)
				.Cycles(out IRealtimeTimer timer)
				.Subscribe(cycles =>
				{
					_resourceOperations.Receive(ResourceId.Energy, cycles);
					timer.FlushCycles();
				});

		[Button(Style = ButtonStyle.FoldoutButton), DisableInEditorMode]
		private void LogResourceTimeLeft(TimedResource resource) =>
			Debug.Log(_timers
				.TimerOf(resource)
				.TimeLeft());

		[Button, DisableInEditorMode] private void Save() => _save.Save();
	}
}