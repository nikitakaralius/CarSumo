using System;
using System.Collections.Generic;
using CarSumo.DataModel.GameResources;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Resources
{
    public class ResourceView : MonoBehaviour
    {
        private const char Separator = '/';

        [SerializeField] private ResourceId _resource;
        [SerializeField] private TMP_Text _text;

        private IResourceStorage _resourceStorage;
        private IEnumerable<IDisposable> _subscriptions; 

        [Inject]
        private void Construct(IResourceStorage resourceStorage)
        {
            _resourceStorage = resourceStorage;
        }

        private void OnEnable()
        {
            IDisposable amountSubscription = _resourceStorage
                .GetResourceAmount(_resource)
                .Subscribe(UpdateResourceAmount);

            IDisposable limitSubscription =_resourceStorage
                .GetResourceLimit(_resource)
                .Subscribe(UpdateResourceLimit);

            _subscriptions = new[] {amountSubscription, limitSubscription};
        }

        private void OnDestroy()
        {
            foreach (IDisposable subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }

        private void UpdateResourceAmount(int amount)
        {
            int? limit = _resourceStorage.GetResourceLimit(_resource).Value;
            _text.text = limit.HasValue ? $"{amount}{Separator}{limit}" : $"{amount}";
        }

        private void UpdateResourceLimit(int? limit)
        {
            int amount = _resourceStorage.GetResourceAmount(_resource).Value;
            _text.text = limit.HasValue ? $"{amount}{Separator}{limit}" : $"{amount}";
        }
    }
}