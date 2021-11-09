using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Services.Timers.Realtime
{
	public class CyclicRealtimeTimer : IRealtimeTimer
	{
		private readonly TimeSpan _cycleDuration;
		private readonly ReactiveProperty<TimeSpan> _timeLeft;
		private readonly Subject<int> _cycleObservable = new Subject<int>();
		
		private static readonly TimeSpan ZeroSpan = TimeSpan.FromSeconds(0);
		private int _cycles;
		
		public CyclicRealtimeTimer(TimeSpan cycleDuration) : this(cycleDuration, cycleDuration, DateTime.Now)
		{
			
		}
		
		public CyclicRealtimeTimer(TimeSpan cycleDuration, TimeSpan timeLeft, DateTime lastSession)
		{
			_cycleDuration = cycleDuration;

			TimeSpan timePassedSinceLastSession = DateTime.Now - lastSession + timeLeft;
			_cycles = (int) (timePassedSinceLastSession.TotalSeconds / cycleDuration.TotalSeconds);

			TimeSpan timeLeftSinceLastSession = timeLeft - DateTime.Now.TimeOfDay;

			_timeLeft = new ReactiveProperty<TimeSpan>(
				timeLeftSinceLastSession.TotalSeconds >= 0
				? timeLeftSinceLastSession
				: ZeroSpan);
		}

		public IReadOnlyReactiveProperty<TimeSpan> TimeLeft() => _timeLeft;

		public IObservable<int> ObserveCycles() => _cycleObservable;

		public async void Start()
		{
			_timeLeft.SetValueAndForceNotify(_cycleDuration);
			await TickAsync(_cycleDuration);
		}

		public void FlushCycles() => _cycles = 0;

		private async Task TickAsync(TimeSpan cycleDuration)
		{
			while (true)
			{
				if (_timeLeft.Value <= ZeroSpan)
				{
					_cycleObservable.OnNext(_cycles++);
					_timeLeft.SetValueAndForceNotify(cycleDuration);
				}
				
				_timeLeft
					.SetValueAndForceNotify(_timeLeft.Value
					.Subtract(TimeSpan.FromSeconds(Time.deltaTime)));
				
				await Task.Yield();
			}
		}
	}
}