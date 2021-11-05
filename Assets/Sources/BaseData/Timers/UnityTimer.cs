using UnityEngine;

namespace BaseData.Timers
{
	public class UnityTimer : ITimer
	{
		private float _startTime;
		private float _duration;

		public bool Elapsed => Time.time >= _startTime + _duration;

		public float PercentElapsed => (Time.time - _startTime) / _duration;

		public void Start(float duration)
		{
			_startTime = Time.time;
			_duration = duration;
		}
	}
}