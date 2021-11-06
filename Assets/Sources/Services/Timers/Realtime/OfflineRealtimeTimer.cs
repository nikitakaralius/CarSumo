using System;
using System.Collections;
using CarSumo.Coroutines;
using UniRx;

namespace Services.Timers.Realtime
{
	public class OfflineRealtimeTimer : IRealtimeTimer, IRealtimeTimerOperations
	{
		private readonly ReactiveProperty<DateTime> _timeLeft;
		private readonly CoroutineExecutor _executor;

		public OfflineRealtimeTimer(DateTime timeLeft, CoroutineExecutor executor)
		{
			_timeLeft = new ReactiveProperty<DateTime>(timeLeft);
			_executor = executor;
		}

		public event Action Elapsed;

		public IReadOnlyReactiveProperty<DateTime> TimeLeft => _timeLeft;

		private DateTime Time => _timeLeft.Value;

		public void Start(DateTime duration)
		{
			_executor.StartCoroutine(TickRoutine());
		}

		private IEnumerator TickRoutine()
		{
			while (Time > DateTime.MinValue)
			{
				_timeLeft.Value = Time.AddSeconds(-UnityEngine.Time.deltaTime);
				yield return null;
			}
			
			Elapsed?.Invoke();
			_timeLeft.Value = DateTime.MinValue;
		}
	}
}