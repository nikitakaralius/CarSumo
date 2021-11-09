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
		private readonly ReactiveProperty<int> _cycles;

		private static readonly TimeSpan ZeroSpan = TimeSpan.FromSeconds(0);

		public CyclicRealtimeTimer(TimeSpan cycleDuration) : this(cycleDuration, cycleDuration, DateTime.Now)
		{
			
		}
		
		public CyclicRealtimeTimer(TimeSpan cycleDuration, TimeSpan timeLeft, DateTime lastSession)
		{
			_cycleDuration = cycleDuration;

			TimeSpan timePassedSinceLastSession = DateTime.Now - lastSession + timeLeft;
			_cycles = new ReactiveProperty<int>((int) (timePassedSinceLastSession.TotalSeconds / cycleDuration.TotalSeconds) - 1);

			_timeLeft = new ReactiveProperty<TimeSpan>(TimeLeftSinceLastSession(cycleDuration, timeLeft, lastSession));
		}

		public IReadOnlyReactiveProperty<TimeSpan> TimeLeft() => _timeLeft;

		public IReadOnlyReactiveProperty<int> Cycles() => _cycles;

		public async void Start() => await TickAsync(_cycleDuration);

		public void FlushCycles() => _cycles.Value = 0;

		private async Task TickAsync(TimeSpan cycleDuration)
		{
			while (true)
			{
				if (_timeLeft.Value <= ZeroSpan)
				{
					_cycles.Value++;
					_timeLeft.SetValueAndForceNotify(cycleDuration);
				}
				
				_timeLeft
					.SetValueAndForceNotify(_timeLeft.Value
					.Subtract(TimeSpan.FromSeconds(Time.deltaTime)));
				
				await Task.Yield();
			}
		}

		private TimeSpan TimeLeftSinceLastSession(TimeSpan cycleDuration, TimeSpan timeLeft, DateTime lastSession) => 
			TimeSpan.FromSeconds(Math.Abs((DateTime.Now - lastSession - timeLeft).TotalSeconds) / cycleDuration.TotalSeconds);
	}
}