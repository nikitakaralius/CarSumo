using System;
using System.Collections.Generic;
using System.Linq;
using Services.Timers.Realtime;

namespace Menu.Resources
{
	public class ResourceTimers
	{
		private readonly IReadOnlyDictionary<TimedResource, IRealtimeTimer> _timers;
		
		public ResourceTimers(IReadOnlyDictionary<TimedResource, IRealtimeTimer> timers)
		{
			_timers = timers;
		}

		public IRealtimeTimer TimerOf(TimedResource resource)
		{
			if (_timers.TryGetValue(resource, out var timer) == false)
				throw new ArgumentOutOfRangeException($"There is no timer using {resource} resource");

			return timer;
		}

		public IEnumerable<(IRealtimeTimer, TimedResource)> All() =>
			_timers.Select(pair => (pair.Value, pair.Key));
	}
}