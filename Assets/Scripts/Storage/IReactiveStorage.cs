using System;

namespace CarSumo.Storage
{
    public interface IReactiveStorage<T> : IStorage<T>
    {
        event Action<T> Added;
        event Action<T> Removed;
        event Action<T> Emptied;
    }
}