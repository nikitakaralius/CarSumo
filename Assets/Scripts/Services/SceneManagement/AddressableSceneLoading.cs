using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Services.SceneManagement
{
    public class AddressableSceneLoading : IAsyncSceneLoading, IAsyncHandleSceneLoading
    {
        private readonly Dictionary<string, SceneInstance> _sceneInstances;

        public AddressableSceneLoading()
        {
            _sceneInstances = new Dictionary<string, SceneInstance>();
        }

        async Task IAsyncSceneLoading.LoadAsync(SceneLoadData sceneLoadData)
        {
            var asyncOperation = Addressables.LoadSceneAsync(sceneLoadData.Name, sceneLoadData.LoadSceneMode);
            
            await asyncOperation.Task;
            
            _sceneInstances.Add(sceneLoadData.Name, asyncOperation.Result);
        }

        async Task IAsyncSceneLoading.UnloadAsync(SceneLoadData sceneLoadData)
        {
            SceneInstance sceneToUnload = _sceneInstances[sceneLoadData.Name];
            var asyncOperation = Addressables.UnloadSceneAsync(sceneToUnload);
            
            await asyncOperation.Task;
            
            _sceneInstances.Remove(sceneLoadData.Name);
        }

        AsyncOperationHandle<SceneInstance> IAsyncHandleSceneLoading.LoadAsync(SceneLoadData sceneLoadData)
        {
            var asyncOperation = Addressables.LoadSceneAsync(sceneLoadData.Name, sceneLoadData.LoadSceneMode);
            asyncOperation.Completed += handle => _sceneInstances.Add(sceneLoadData.Name, handle.Result);
            return asyncOperation;
        }

        AsyncOperationHandle<SceneInstance> IAsyncHandleSceneLoading.UnloadAsync(SceneLoadData sceneLoadData)
        {
            SceneInstance sceneToUnload = _sceneInstances[sceneLoadData.Name];
            var asyncOperation = Addressables.UnloadSceneAsync(sceneToUnload);
            asyncOperation.Completed += _ => _sceneInstances.Remove(sceneLoadData.Name);
            return asyncOperation;
        }
    }
}