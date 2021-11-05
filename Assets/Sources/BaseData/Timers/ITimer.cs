namespace BaseData.Timers
{
	public interface ITimer
	{
		bool Elapsed { get; }
		float PercentElapsed { get; }
		void Start(float duration);
	}
}