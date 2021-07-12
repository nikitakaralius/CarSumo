
using CarSumo.Infrastructure.Services.SceneManagement;
using UnityEngine.SceneManagement;

namespace CarSumo.Infrastructure.Services.LoadingScreen
{
    public class SceneLoadingScreen : ILoadingScreen
    {
        private const string LoadingScreen = "LoadingScreen";
        private readonly ISceneLoadService _loadService;
        private readonly SceneLoadData _loadScreenScene;

        public SceneLoadingScreen(ISceneLoadService loadService)
        {
            _loadService = loadService;
            _loadScreenScene = new SceneLoadData(LoadingScreen, LoadSceneMode.Additive);
        }


        public void Enable()
        {
            _loadService.Load(_loadScreenScene);
        }

        public void Disable()
        {
            _loadService.Unload(_loadScreenScene);
        }
    }
}