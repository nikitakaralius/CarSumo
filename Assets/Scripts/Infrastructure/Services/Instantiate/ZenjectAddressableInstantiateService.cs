using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CarSumo.Infrastructure.Services.Instantiate
{
    public class ZenjectAddressablesInstantiate : IAddressablesInstantiate
    {
        private readonly DiContainer _container;

        public ZenjectAddressablesInstantiate(DiContainer container)
        {
            _container = container;
        }

        public async Task<T> InstantiateAsync<T>(AssetReferenceGameObject reference, Transform parent) where T : Component
        {
            GameObject resource = reference.IsValid() ?
                GetReferenceAsset(reference) : 
                await reference.LoadAssetAsync<GameObject>().Task;
            
            T instance = _container.InstantiatePrefabForComponent<T>(resource, parent);
            return instance;
        }

        private GameObject GetReferenceAsset(AssetReferenceGameObject reference)
        {
            #if UNITY_EDITOR
            return reference.editorAsset;
            #endif

#pragma warning disable 162
            return (GameObject)reference.Asset;
#pragma warning restore 162
        }
    }
}