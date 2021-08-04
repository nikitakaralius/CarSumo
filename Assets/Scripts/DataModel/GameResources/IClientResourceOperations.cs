namespace CarSumo.DataModel.GameResources
{
    public interface IClientResourceOperations
    {
        void Receive(ResourceId id, int amount);
        bool TrySpend(ResourceId id, int amount);
    }
}