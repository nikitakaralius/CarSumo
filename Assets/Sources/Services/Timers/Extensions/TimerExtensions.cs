using System;
using System.Runtime.CompilerServices;
using Services.Timer.InGameTimer;

namespace Services.Timers.Extensions
{
	public static class TimerExtensions
	{
		public static TimerElapsedAwaiter GetAwaiter(this ITimer timer) => new TimerElapsedAwaiter(timer);
	}

	public struct TimerElapsedAwaiter : INotifyCompletion
	{
		private readonly ITimer _timer;
		private Action _continuation;

		public TimerElapsedAwaiter(ITimer timer)
		{
			_timer = timer;
			_continuation = null;
			IsCompleted = false;
		}
		
		public bool IsCompleted { get; private set; }

		public void GetResult() { }

		public void OnCompleted(Action continuation)
		{
			_continuation = continuation;
			_timer.Elapsed += OnTimerElapsed;
		}

		private void OnTimerElapsed()
		{
			IsCompleted = true;
			_continuation.Invoke();
			_timer.Elapsed -= OnTimerElapsed;
		}
	}
}