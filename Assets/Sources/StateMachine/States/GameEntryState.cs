using Services.SceneManagement;
using UnityEngine.SceneManagement;
using Zenject;

namespace CarSumo.StateMachine.States
{
    public class GameEntryState : IState
    {
        private readonly IAsyncSceneLoading _sceneLoading;
        private readonly LazyInject<GameStateMachine> _stateMachine;
        
        public GameEntryState(IAsyncSceneLoading sceneLoading, LazyInject<GameStateMachine> stateMachine)
        {
            _sceneLoading = sceneLoading;
            _stateMachine = stateMachine;
        }

        private SceneLoadData Game => new SceneLoadData("Game", LoadSceneMode.Single);
        private SceneLoadData Ui => new SceneLoadData("GameUI", LoadSceneMode.Additive);
        
        public async void Enter()
        {
            await _sceneLoading.LoadAsync(Game);
            await _sceneLoading.LoadAsync(Ui);
            
            _stateMachine.Value.Enter<GameState>();
        }

        public void Exit()
        {
        }
    }
}