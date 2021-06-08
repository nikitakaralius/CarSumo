namespace CarSumo.Storage
{
    public interface IStorage<T>
    {
        void Add(T element);
        void Remove(T element);
    }
}