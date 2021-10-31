using System;
using System.Collections.Generic;
using CarSumo.DataModel.GameResources;
using UniRx;
using UnityEngine;

namespace DataModel.GameData.Resources
{
    public class GameResourceStorage : IResourceStorage, IClientResourceOperations, IResourceStorageMessages
    {
        private readonly Dictionary<ResourceId, ReactiveProperty<int>> _resourceAmounts;
        private readonly Dictionary<ResourceId, ReactiveProperty<int?>> _resourceLimits;

        private readonly Subject<ResourceId> _resourceChangedObserver = new Subject<ResourceId>();
        
        public GameResourceStorage(Dictionary<ResourceId, ReactiveProperty<int>> resourceAmounts,
                                   Dictionary<ResourceId, ReactiveProperty<int?>> resourceLimits)
        {
            _resourceAmounts = resourceAmounts;
            _resourceLimits = resourceLimits;
        }

        public IReadOnlyReactiveProperty<int> GetResourceAmount(ResourceId id)
        {
            if (_resourceAmounts.TryGetValue(id, out var amount))
            {
                return amount;
            }
            throw new ArgumentOutOfRangeException($"Trying to get unregistred resource {id} amount");
        }

        public IReadOnlyReactiveProperty<int?> GetResourceLimit(ResourceId id)
        {
            if (_resourceLimits.TryGetValue(id, out var limit))
            {
                return limit;
            }
            throw new ArgumentOutOfRangeException($"Trying to get unregistred resource {id} limit");
        }

        public void Receive(ResourceId id, int amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException($"Trying to receive negative amount {amount}");
            }
            if (_resourceAmounts.TryGetValue(id, out var currentAmount) == false)
            {
                _resourceAmounts[id] = currentAmount = new ReactiveProperty<int>(0);
            }

            ClampResourceLimit();
            _resourceChangedObserver.OnNext(id);
            
            void ClampResourceLimit()
            {
                if (_resourceLimits.TryGetValue(id, out var limit))
                {
                    int? limitValue = limit.Value;
                    if (limitValue.HasValue)
                    {
                        currentAmount.Value = Mathf.Clamp(currentAmount.Value + amount, 0, limitValue.Value);
                        return;
                    }
                }

                currentAmount.Value += amount;
            }
        }

        public bool TrySpend(ResourceId id, int amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException($"Trying to spend negative amount {amount}");
            }

            if (_resourceAmounts.TryGetValue(id, out var currentAmount))
            {
                if (currentAmount.Value >= amount)
                {
                    currentAmount.Value -= amount;
                    _resourceChangedObserver.OnNext(id);
                    return true;
                }
            }
            return false;
        }

        public IObservable<ResourceId> ObserveResourceChanged()
        {
	        return _resourceChangedObserver;
        }
    }
}