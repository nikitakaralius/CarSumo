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
                {ResourceId.Gold, new ReactiveProperty<int>(500)},
                {ResourceId.Gems, new ReactiveProperty<int>(5)},
                {ResourceId.AccountSlots, new ReactiveProperty<int>(4)}
            };
            var limits = new Dictionary<ResourceId, ReactiveProperty<int?>>()
            {
                {ResourceId.Energy, new ReactiveProperty<int?>(25)},
                {ResourceId.Gold, new ReactiveProperty<int?>(null)},
                {ResourceId.Gems, new ReactiveProperty<int?>(null)},
                {ResourceId.AccountSlots, new ReactiveProperty<int?>(16)}
            };

            return new GameResourceStorage(amounts, limits);
        }
    }
}