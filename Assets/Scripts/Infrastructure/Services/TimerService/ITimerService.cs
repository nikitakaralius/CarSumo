using System;

namespace CarSumo.Infrastructure.Services.TimerService
{
    public interface ITimerService
    {
        event Action Elapsed;
        float Seconds { get; }
        void Start();
        void Stop();
    }
}