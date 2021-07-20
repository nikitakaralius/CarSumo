using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CarSumo.Infrastructure.Services.LoadingScreen
{
    public class PrefabLoadingScreen : ILoadingScreen
    {
        private const string LoadingScreenPrefab = "LoadingScreenPrefab";
        private GameObject _instance;
        
        public async Task Enable()
        {
            var instantiateOperation =Addressables.InstantiateAsync(LoadingScreenPrefab);
            await instantiateOperation.Task;
            _instance = instantiateOperation.Result;
        }

        public Task Disable()
        {
            return Task.Run(() => GameObject.Destroy(_instance));
        }
    }
}