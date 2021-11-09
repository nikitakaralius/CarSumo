using System;
using UniRx;

namespace Services.Timers.Realtime
{
	public interface IRealtimeTimer
	{
		IReadOnlyReactiveProperty<TimeSpan> TimeLeft { get; }
		IObservable<int> ObserveCycles();
		void Start();
		void FlushCycles();
	}
}