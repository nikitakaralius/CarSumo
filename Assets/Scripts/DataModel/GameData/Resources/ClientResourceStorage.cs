using System;
using System.Collections.Generic;
using CarSumo.DataModel.GameResources;
using DataModel.FileData;
using UniRx;
using UnityEngine;
using Zenject;

namespace DataModel.GameData.Resources
{
    public class ClientResourceStorage : IResourceStorage, IClientResourceOperations, IInitializable
    {
        private readonly Dictionary<ResourceId, ReactiveProperty<int>> _resourceAmounts;
        private readonly Dictionary<ResourceId, ReactiveProperty<int?>> _resourceLimits;

        private readonly IFileService _fileService;
        private readonly IResourcesConfig _config;
        
        public ClientResourceStorage(IFileService fileService, IResourcesConfig config)
        {
            _fileService = fileService;
            _config = config;

            _resourceAmounts = new Dictionary<ResourceId, ReactiveProperty<int>>();
            _resourceLimits = new Dictionary<ResourceId, ReactiveProperty<int?>>();
        }

        public void Initialize()
        {
            var resources = _fileService.Load<SerializableResources>(_config.FilePath).Storage;
            
            foreach (KeyValuePair<ResourceId,(int, int?)> resource in resources)
            {
                _resourceAmounts.Add(resource.Key, new ReactiveProperty<int>(resource.Value.Item1));
                _resourceLimits.Add(resource.Key, new ReactiveProperty<int?>(resource.Value.Item2));
            }
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
                    return true;
                }
            }
            return false;
        }
    }
}