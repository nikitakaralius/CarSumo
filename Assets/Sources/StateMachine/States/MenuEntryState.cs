using Services.SceneManagement;
using UnityEngine.SceneManagement;
using Zenject;

namespace CarSumo.StateMachine.States
{
    public class MenuEntryState : IState
    {
        private readonly IAsyncSceneLoading _sceneLoading;
        private readonly LazyInject<GameStateMachine> _stateMachine;

        public MenuEntryState(IAsyncSceneLoading sceneLoading, LazyInject<GameStateMachine> stateMachine)
        {
            _sceneLoading = sceneLoading;
            _stateMachine = stateMachine;
        }
        
        private SceneLoadData Menu => new SceneLoadData("Menu", LoadSceneMode.Single);

        public async void Enter()
        {
            await _sceneLoading.LoadAsync(Menu);
            _stateMachine.Value.Enter<MenuState>();
        }

        public void Exit()
        {
            
        }
    }
}