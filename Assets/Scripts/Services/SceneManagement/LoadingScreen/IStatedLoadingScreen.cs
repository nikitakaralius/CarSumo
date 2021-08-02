using UnityEngine.ResourceManagement.AsyncOperations;

namespace Services.SceneManagement.LoadingScreen
{
    public interface IStatedLoadingScreen
    {
        void Enable(AsyncOperationHandle loadHandle);
        void Disable();
    }
}