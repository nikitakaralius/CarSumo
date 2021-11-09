using System;
using UniRx;

namespace Services.Timers.Realtime
{
	public interface IRealtimeTimer
	{
		IReadOnlyReactiveProperty<TimeSpan> TimeLeft();
		IObservable<int> ObserveCycles();
		void Start();
		void FlushCycles();
	}
}