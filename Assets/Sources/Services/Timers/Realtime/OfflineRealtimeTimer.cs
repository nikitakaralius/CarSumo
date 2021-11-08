using System;
using System.Threading.Tasks;
using UniRx;
using UnityTime = UnityEngine.Time;

namespace Services.Timers.Realtime
{
	public class OfflineRealtimeTimer : IRealtimeTimer
	{
		private readonly ReactiveProperty<DateTime> _timeLeft;

		public OfflineRealtimeTimer(DateTime timeLeft)
		{
			_timeLeft = new ReactiveProperty<DateTime>(timeLeft);
		}

		public event Action Elapsed;

		public IReadOnlyReactiveProperty<DateTime> TimeLeft => _timeLeft;

		private DateTime Time => _timeLeft.Value;

		public async void Start(DateTime duration)
		{
			_timeLeft.Value = duration;
			await TickAsync();
		}

		private async Task TickAsync()
		{
			while (Time > DateTime.MinValue)
			{
				_timeLeft.Value = Time.AddSeconds(-UnityTime.deltaTime);
				await Task.Yield();
			}
			
			Elapsed?.Invoke();
			_timeLeft.Value = DateTime.MinValue;
		}
	}
}