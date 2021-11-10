using System;
using UniRx;

namespace Services.Timers.Realtime
{
	public interface IRealtimeTimer
	{
		IReadOnlyReactiveProperty<TimeSpan> TimeLeft();
		IReadOnlyReactiveProperty<int> Cycles();
		void Start();
		void FlushCycles();
	}
}