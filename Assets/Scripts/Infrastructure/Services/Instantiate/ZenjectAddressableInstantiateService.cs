using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CarSumo.Infrastructure.Services.Instantiate
{
    public class ZenjectAddressableInstantiateService : IInstantiateService
    {
        private readonly DiContainer _container;

        public ZenjectAddressableInstantiateService(DiContainer container)
        {
            _container = container;
        }

        public async Task<T> InstantiateAsync<T>(object key) where T : Component
        {
            return await InstantiateAsync<T>(key, null);
        }

        public async Task<T> InstantiateAsync<T>(object key, Transform parent) where T : Component
        {
            T component = await Addressables.LoadAssetAsync<T>(key).Task;
            T instance = _container.InstantiatePrefabForComponent<T>(component.gameObject, parent);
            return instance;
        }

        public async Task<T> InstantiateAsync<T>(object key, Vector3 position, Quaternion rotation) where T : Component
        {
            return await InstantiateAsync<T>(key, position, rotation, null);
        }

        public async Task<T> InstantiateAsync<T>(object key,
            Vector3 position,
            Quaternion rotation,
            Transform parent) where T : Component
        {
            T component = await Addressables.LoadAssetAsync<T>(key).Task;
            T instance = _container.InstantiatePrefabForComponent<T>(component.gameObject, position, rotation, parent);
            return instance;
        }
    }
}