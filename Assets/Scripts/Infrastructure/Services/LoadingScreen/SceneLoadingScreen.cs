using CarSumo.Infrastructure.Services.SceneManagement;
using System.Threading.Tasks;
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

        public async Task Enable()
        { 
            await _loadService.Load(_loadScreenScene);
        }

        public async Task Disable()
        {
            await _loadService.Unload(_loadScreenScene);
        }
    }
}