using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Menu.Resources
{
	[CreateAssetMenu(fileName = "TimersConfiguration", menuName = "Resources/Timers/Configuration")]
	public class TimersConfiguration : SerializedScriptableObject
	{
		[Serializable]
		private struct SerializableTimeSpan
		{
			[Min(0)] public int Days;
			[Min(0)] public int Hours;
			[Min(0)] public int Minutes;
			[Min(0)] public int Seconds;

			public TimeSpan ToTimeSpan() => 
				new TimeSpan(Days, Hours, Minutes, Seconds);
		}

		[SerializeField] private IReadOnlyDictionary<TimedResource, SerializableTimeSpan> _resourceRollbackDurations = new Dictionary<TimedResource, SerializableTimeSpan>();

		public TimeSpan RollbackDurationOf(TimedResource resource)
		{
			if (_resourceRollbackDurations.TryGetValue(resource, out var rollbackDuration) == false)
				throw new ArgumentOutOfRangeException(
					$"Unable to detect {resource} duration. Check TimersConfiguration SO");

			return rollbackDuration.ToTimeSpan();
		}
	}
}