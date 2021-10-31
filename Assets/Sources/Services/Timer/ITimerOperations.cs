namespace Services.Timer
{
    public interface ITimerOperations
    {
        void Start(float secondsToElapse);
        void Stop();
    }
}