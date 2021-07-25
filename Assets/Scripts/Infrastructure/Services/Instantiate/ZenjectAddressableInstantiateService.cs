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

        public async Task<T> InstantiateAsync<T>(AssetReference reference, Transform parent) where T : Component
        {
            Object resource = reference.IsValid()
                ? reference.editorAsset : 
                await reference.LoadAssetAsync<T>().Task;
            
            T component = _container.InstantiatePrefabForComponent<T>(resource, parent);
            return component;
        }
    }
}