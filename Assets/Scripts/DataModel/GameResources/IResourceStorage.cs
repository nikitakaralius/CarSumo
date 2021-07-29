using UniRx;

namespace CarSumo.DataModel.GameResources
{
    public interface IResourceStorage
    {
        IReadOnlyReactiveCollection<ResourceId> ResourceTypes { get; }
        
        IReadOnlyReactiveProperty<int> GetResourceAmount(ResourceId id);

        IReadOnlyReactiveProperty<int?> GetResourceLimit(ResourceId id);
    }
}