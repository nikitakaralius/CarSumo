using System.Threading.Tasks;
using Services.SceneManagement;
using UnityEngine.SceneManagement;

namespace CarSumo.StateMachine.States
{
    public class GameEntryState : IState
    {
        private readonly IAsyncSceneLoading _sceneLoading;

        public GameEntryState(IAsyncSceneLoading sceneLoading)
        {
            _sceneLoading = sceneLoading;
        }

        private SceneLoadData Game => new SceneLoadData("Game", LoadSceneMode.Single);
        private SceneLoadData Ui => new SceneLoadData("GameUI", LoadSceneMode.Additive);
        
        public async void Enter()
        {
            await Task.WhenAll(_sceneLoading.LoadAsync(Game), _sceneLoading.LoadAsync(Ui));
        }

        public void Exit()
        {
        }
    }
}