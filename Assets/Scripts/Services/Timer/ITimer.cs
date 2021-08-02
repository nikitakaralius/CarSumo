using System;

namespace Services.Timer
{
    public interface ITimer
    {
        float SecondsLeft { get; }
        event Action Elapsed;
    }
}