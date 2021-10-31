using UniRx;

namespace CarSumo.DataModel.GameResources
{
    public interface IResourceStorage
    {
        IReadOnlyReactiveProperty<int> GetResourceAmount(ResourceId id);

        IReadOnlyReactiveProperty<int?> GetResourceLimit(ResourceId id);
    }
}