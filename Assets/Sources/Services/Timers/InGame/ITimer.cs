using System;
using UniRx;

namespace Services.Timer.InGameTimer
{
    public interface ITimer
    {
        IReadOnlyReactiveProperty<float> SecondsLeft { get; }
        event Action Elapsed;
    }
}