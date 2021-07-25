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

        public async Task<T> InstantiateAsync<T>(AssetReferenceGameObject reference, Transform parent) where T : Component
        {
            GameObject resource = reference.IsValid() ?
                GetReferenceAsset(reference) : 
                await reference.LoadAssetAsync<GameObject>().Task;
            
            T component = resource.GetComponent<T>();
            T instance = _container.InstantiatePrefabForComponent<T>(component, parent);
            return instance;
        }

        private GameObject GetReferenceAsset(AssetReferenceGameObject reference)
        {
            #if UNITY_EDITOR
            return reference.editorAsset;
            #endif

            return (GameObject)reference.Asset;
        }
    }
}