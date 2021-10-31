namespace CarSumo.DataModel.GameResources
{
    public interface IServerResourceOperations
    {
        void SetAmount(ResourceId id, int amount);
        void SetLimit(ResourceId id, int? limit);
    }
}