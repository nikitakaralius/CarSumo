using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Services.Timers.Realtime
{
	public class OfflineRealtimeTimer : IRealtimeTimer
	{
		private const int CycleStep = 1;
		
		private readonly TimeSpan _duration;
		private readonly ReactiveProperty<TimeSpan> _timeLeft;
		private readonly Subject<int> _cycles = new Subject<int>();
		
		private static readonly TimeSpan ZeroSpan = TimeSpan.FromSeconds(0);

		public OfflineRealtimeTimer(TimeSpan duration) : this(duration, duration, DateTime.Now)
		{
			
		}
		
		public OfflineRealtimeTimer(TimeSpan duration, TimeSpan timeLeft, DateTime lastSession)
		{
			_duration = duration;

			TimeSpan timePassedSinceLastSession = DateTime.Now - lastSession + timeLeft;
			_cycles.OnNext((int) (timePassedSinceLastSession.TotalSeconds / duration.TotalSeconds));

			TimeSpan timeLeftSinceLastSession = timeLeft - DateTime.Now.TimeOfDay;

			_timeLeft = new ReactiveProperty<TimeSpan>(
				timeLeftSinceLastSession.TotalSeconds >= 0
				? timeLeftSinceLastSession
				: ZeroSpan);
		}

		public IReadOnlyReactiveProperty<TimeSpan> TimeLeft => _timeLeft;

		public IObservable<int> ObserveCycles() => _cycles;

		public async void Start()
		{
			_timeLeft.SetValueAndForceNotify(_duration);
			await TickAsync();
		}

		private async Task TickAsync()
		{
			while (_timeLeft.Value >= ZeroSpan)
			{
				_timeLeft
					.SetValueAndForceNotify(_timeLeft.Value
					.Subtract(TimeSpan.FromSeconds(Time.deltaTime)));
				
				await Task.Yield();
			}
			
			_cycles.OnNext(CycleStep);
			_timeLeft.SetValueAndForceNotify(ZeroSpan);
		}
	}
}