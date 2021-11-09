using Services.Timers.Realtime;
using UniRx;

namespace Services.Timers.Extensions
{
	public static class RealtimeTimerExtensions
	{
		public static IReadOnlyReactiveProperty<int> Cycles(this IRealtimeTimer timer, out IRealtimeTimer instance)
		{
			instance = timer;
			return timer.Cycles();
		}
	}
}