using System;
using UniRx;

namespace Services.Timers.Realtime
{
	public interface IRealtimeTimer
	{
		event Action Elapsed;
		IReadOnlyReactiveProperty<DateTime> TimeLeft { get; }
	}

	public interface IRealtimeTimerOperations
	{
		void Start(DateTime duration);
	}
}