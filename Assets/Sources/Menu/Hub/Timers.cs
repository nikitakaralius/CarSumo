using System;
using System.Collections.Generic;
using CarSumo.Extensions;
using DataModel.GameData.GameSave;
using Menu.Resources;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Menu.Hub
{
	public class Timers : SerializedMonoBehaviour
	{
		[Serializable]
		private struct SerializedDateTime
		{
			[Min(0)] public float Days;
			[Min(0)] public float Hours;
			[Min(0)] public float Minutes;
			[Min(0)] public float Seconds;

			public DateTime ToDateTime() =>
				new DateTime()
					.AddDays(Days)
					.AddHours(Hours)
					.AddMinutes(Minutes)
					.AddSeconds(Seconds);
		}

		[SerializeField]
		private IReadOnlyDictionary<TimedResource, SerializedDateTime> _resourceRollbackTime = new Dictionary<TimedResource, SerializedDateTime>();

		private ResourceTimers _timers;
		private ResourceTimersSave _timersSave;

		[Inject]
		private void Construct(ResourceTimers timers, ResourceTimersSave timersSave)
		{
			_timers = timers;
			_timersSave = timersSave;
		}
		
		[Button, DisableInEditorMode]
		public void Boot()
		{
			foreach (var (resource, time) in _resourceRollbackTime)
				_timers
					.TimerOf(resource)
					.Start(time.ToDateTime());
		}

		[Button(ButtonStyle.FoldoutButton), DisableInEditorMode]
		private void LogResourceTime(TimedResource resource) => Debug.Log(_timers.TimerOf(resource).TimeLeft);

		[Button, DisableInEditorMode]
		private void SaveTimers() => _timersSave.Save();
	}
}