using System;
using UniRx;

namespace Services.Timer
{
    public interface ITimer
    {
        IReadOnlyReactiveProperty<float> SecondsLeft { get; }
        event Action Elapsed;
    }
}