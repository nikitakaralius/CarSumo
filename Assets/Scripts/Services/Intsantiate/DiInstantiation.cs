using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Services.Instantiate
{
    public class DiInstantiation : IAsyncInstantiation
    {
        private readonly DiContainer _container;

        public DiInstantiation(DiContainer container)
        {
            _container = container;
        }

        public async Task<T> InstantiateAsync<T>(AssetReferenceGameObject asset, Transform parent = null) where T : Component
        {
            GameObject resource = asset.IsValid()
                ? GetReferencedAsset(asset)
                : await asset.LoadAssetAsync<GameObject>().Task;

            T component = _container.InstantiatePrefabForComponent<T>(resource, parent);
            return component;
        }

        private GameObject GetReferencedAsset(AssetReferenceGameObject reference)
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