namespace Services.Timer.InGameTimer
{
    public interface ITimerOperations
    {
        void Start(float secondsToElapse);
        void Stop();
    }
}