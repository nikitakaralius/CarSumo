using System;
using UniRx;
using Zenject;

namespace Services.Timers.Realtime
{
	public interface IRealtimeTimer : ITickable
	{
		IReadOnlyReactiveProperty<TimeSpan> TimeLeft();
		IReadOnlyReactiveProperty<int> Cycles();
		void Start();
		void Stop();
		void FlushCycles();
	}
}