using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Services.SceneManagement
{
    public interface IAsyncHandleSceneLoading
    {
        AsyncOperationHandle<SceneInstance> LoadAsync(SceneLoadData sceneLoadData);
        AsyncOperationHandle<SceneInstance> UnloadAsync(SceneLoadData sceneLoadData);
    }
}