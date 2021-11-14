using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Services.Timers.Realtime
{
	public class CyclicRealtimeTimer : IRealtimeTimer
	{
		private readonly TimeSpan _cycleDuration;
		private readonly ReactiveProperty<TimeSpan> _timeLeft;
		private readonly ReactiveProperty<int> _cycles;

		private ITickable _tickable = new FakeTickable();

		public CyclicRealtimeTimer(TimeSpan cycleDuration)
		{
			_cycleDuration = cycleDuration;
			_cycles = new ReactiveProperty<int>(0);
			_timeLeft = new ReactiveProperty<TimeSpan>(_cycleDuration);
		}
		public CyclicRealtimeTimer(TimeSpan cycleDuration, TimeSpan timeLeft, DateTime lastSession)
		{
			_cycleDuration = cycleDuration;

			TimeSpan timePassedSinceLastSession = DateTime.Now - lastSession + timeLeft;
			_cycles = new ReactiveProperty<int>((int) (timePassedSinceLastSession.TotalSeconds / cycleDuration.TotalSeconds));

			_timeLeft = new ReactiveProperty<TimeSpan>(TimeLeftSinceLastSession(cycleDuration, timeLeft, lastSession));
		}

		public IReadOnlyReactiveProperty<TimeSpan> TimeLeft() => _timeLeft;

		public IReadOnlyReactiveProperty<int> Cycles() => _cycles;

		public void Start() => _tickable = new CyclicRealtimeTimer.Tickable(this);
		public void Stop() => _tickable = new FakeTickable();

		public void Tick() => _tickable.Tick();

		public void FlushCycles() => _cycles.Value = 0;

		private TimeSpan TimeLeftSinceLastSession(TimeSpan cycleDuration, TimeSpan timeLeft, DateTime lastSession) => 
			TimeSpan.FromSeconds(Math.Abs((DateTime.Now - lastSession - timeLeft).TotalSeconds) % cycleDuration.TotalSeconds);

		private class Tickable : ITickable
		{
			private static readonly TimeSpan ZeroSpan = TimeSpan.FromSeconds(0);

			private readonly CyclicRealtimeTimer _timer;

			public Tickable(CyclicRealtimeTimer timer)
			{
				_timer = timer;
			}
			
			private ReactiveProperty<TimeSpan> TimeLeft => _timer._timeLeft;

			public void Tick()
			{
				if (TimeLeft.Value <= ZeroSpan)
				{
					_timer._cycles.Value++;
					TimeLeft.SetValueAndForceNotify(_timer._cycleDuration);
				}
				else
				{
					TimeLeft
						.SetValueAndForceNotify(TimeLeft.Value
							.Subtract(TimeSpan.FromSeconds(Time.deltaTime)));
				}
			}
		}
	}
}