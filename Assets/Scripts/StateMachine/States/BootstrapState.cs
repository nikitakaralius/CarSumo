using Services.SceneManagement;
using UnityEngine.SceneManagement;

namespace CarSumo.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IAsyncSceneLoading _sceneLoading;

        public BootstrapState(IAsyncSceneLoading sceneLoading)
        {
            _sceneLoading = sceneLoading;
        }

        private SceneLoadData Menu => new SceneLoadData("Menu", LoadSceneMode.Single);

        public async void Enter()
        {
            await _sceneLoading.LoadAsync(Menu);
        }

        public void Exit()
        {
            
        }
    }
}