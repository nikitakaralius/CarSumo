using System;
using UniRx;

namespace Services.Timers.Realtime
{
	public interface IRealtimeTimer
	{
		IReadOnlyReactiveProperty<TimeSpan> TimeLeft { get; }
		void Start();
		IObservable<int> ObserveCycles();
	}
}