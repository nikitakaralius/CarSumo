using System;
using System.Collections.Generic;
using System.Linq;
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

        private readonly IAsyncFileService _fileService;
        private readonly IResourcesConfiguration _configuration;
        
        public ClientResourceStorage(IAsyncFileService fileService, IResourcesConfiguration configuration)
        {
            _fileService = fileService;
            _configuration = configuration;

            _resourceAmounts = new Dictionary<ResourceId, ReactiveProperty<int>>();
            _resourceLimits = new Dictionary<ResourceId, ReactiveProperty<int?>>();
        }

        public async void Initialize()
        {
            SerializableResources resources = await _fileService.LoadAsync<SerializableResources>(_configuration.FilePath);
            
            foreach (KeyValuePair<ResourceId,int> amount in resources.Amounts)
            {
                _resourceAmounts.Add(amount.Key, new ReactiveProperty<int>(amount.Value));
            }
            foreach (KeyValuePair<ResourceId,int?> limit in resources.Limits)
            {
                _resourceLimits.Add(limit.Key, new ReactiveProperty<int?>(limit.Value));
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
            _fileService.SaveAsync(ToSerializableResources(this), _configuration.FilePath);
            
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
                    _fileService.SaveAsync(ToSerializableResources(this), _configuration.FilePath);
                    return true;
                }
            }
            return false;
        }

        private SerializableResources ToSerializableResources(ClientResourceStorage storage)
        {
            Dictionary<ResourceId, int> amounts = storage._resourceAmounts
                .ToDictionary(amount => amount.Key, amount => amount.Value.Value);

            Dictionary<ResourceId, int?> limits = storage._resourceLimits
                .ToDictionary(limit => limit.Key, limit => limit.Value.Value);

            return new SerializableResources()
            {
                Amounts = amounts,
                Limits = limits
            };
        }
    }
}