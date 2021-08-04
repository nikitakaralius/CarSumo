using System.Threading.Tasks;

namespace Services.SceneManagement
{
    public interface IAsyncSceneLoading
    {
        Task LoadAsync(SceneLoadData sceneLoadData);
        Task UnloadAsync(SceneLoadData sceneLoadData);
    }
}