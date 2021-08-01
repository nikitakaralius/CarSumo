using System.Collections.Generic;
using CarSumo.DataModel.GameResources;
using UniRx;

namespace DataModel.GameData.Resources
{
    public class InitialGameResourceStorage : IInitialResourceStorageProvider
    {
        public GameResourceStorage GetInitialStorage()
        {
            var amounts = new Dictionary<ResourceId, ReactiveProperty<int>>()
            {
                {ResourceId.Energy, new ReactiveProperty<int>(25)},
                {ResourceId.Gold, new ReactiveProperty<int>(300)},
                {ResourceId.Gems, new ReactiveProperty<int>(10)}
            };
            var limits = new Dictionary<ResourceId, ReactiveProperty<int?>>()
            {
                {ResourceId.Energy, new ReactiveProperty<int?>(25)},
                {ResourceId.Gold, new ReactiveProperty<int?>(null)},
                {ResourceId.Gems, new ReactiveProperty<int?>(null)}
            };

            return new GameResourceStorage(amounts, limits);
        }
    }
}