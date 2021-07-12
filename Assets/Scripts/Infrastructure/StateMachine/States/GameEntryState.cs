using System.Threading.Tasks;
using CarSumo.Infrastructure.SceneManagement;
using UnityEngine.SceneManagement;

namespace CarSumo.Infrastructure.StateMachine.States
{
    public class GameEntryState : IState
    {
        private const string UI = "UI";
        private const string Game = "Game";
        private readonly ISceneLoadService _loadService;

        public GameEntryState(ISceneLoadService loadService)
        {
            _loadService = loadService;
        }

        public async void Enter()
        {
            var gameScene = new SceneLoadData(Game, LoadSceneMode.Single);
            var uiScene = new SceneLoadData(UI, LoadSceneMode.Additive);

            await Task.WhenAll(_loadService.Load(gameScene), _loadService.Load(uiScene));
        }

        public void Exit()
        {
            
        }
    }
}