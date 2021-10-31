using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.GameResources;
using UniRx;

namespace DataModel.GameData.Resources.Binding
{
    public class ResourceStorageBinding : IResourceStorageBinding
    {
        public GameResourceStorage BindFrom(SerializableResources resources)
        {
            Dictionary<ResourceId, ReactiveProperty<int>> amounts = resources.Amounts
                .ToDictionary(amount => amount.Key, amount => new ReactiveProperty<int>(amount.Value));

            Dictionary<ResourceId, ReactiveProperty<int?>> limits = resources.Limits
                .ToDictionary(limit => limit.Key, limit => new ReactiveProperty<int?>(limit.Value));

            return new GameResourceStorage(amounts, limits);
        }
    }
}