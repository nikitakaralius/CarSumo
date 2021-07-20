using System.Threading.Tasks;
using CarSumo.Infrastructure.Services.LoadingScreen;
using CarSumo.Infrastructure.Services.SceneManagement;
using UnityEngine.SceneManagement;

namespace CarSumo.Infrastructure.StateMachine.States
{
    public class GameEntryState : IState
    {
        private const string UI = "GameUI";
        private const string Game = "Game";

        private readonly ISceneLoadService _loadService;
        private readonly ILoadingScreen _loadingScreen;

        public GameEntryState(ISceneLoadService loadService, ILoadingScreen loadingScreen)
        {
            _loadService = loadService;
            _loadingScreen = loadingScreen;
        }

        public async void Enter()
        {
            await _loadingScreen.Enable();
            await LoadScenes();
        }

        private Task LoadScenes()
        {
            var gameScene = new SceneLoadData(Game, LoadSceneMode.Single);
            var uiScene = new SceneLoadData(UI, LoadSceneMode.Additive);

            Task whenAllScenesLoaded = Task.WhenAll(_loadService.Load(gameScene), _loadService.Load(uiScene));
            return whenAllScenesLoaded;
        }

        public void Exit()
        {
               
        }
    }
}