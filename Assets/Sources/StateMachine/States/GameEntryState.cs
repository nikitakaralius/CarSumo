using CarSumo.DataModel.GameResources;
using Services.SceneManagement;
using UnityEngine.SceneManagement;
using Zenject;

namespace CarSumo.StateMachine.States
{
    public class GameEntryState : IState
    {
        private readonly IAsyncSceneLoading _sceneLoading;
        private readonly IResourceConsumption _resourceConsumption;
        private readonly LazyInject<GameStateMachine> _stateMachine;

        public GameEntryState(IAsyncSceneLoading sceneLoading,
                                IResourceConsumption resourceConsumption,
                                LazyInject<GameStateMachine> stateMachine)
        {
            _sceneLoading = sceneLoading;
            _stateMachine = stateMachine;
            _resourceConsumption = resourceConsumption;
        }

        private SceneLoadData Game => new SceneLoadData("Game", LoadSceneMode.Single);
        private SceneLoadData Ui => new SceneLoadData("GameUI", LoadSceneMode.Additive);
        
        public async void Enter()
        {
            if (_resourceConsumption.ConsumeIfEnoughToEnterGame() == false)
            {
                _stateMachine.Value.Enter<MenuState>();
                return;
            }
            
            await _sceneLoading.LoadAsync(Game);
            await _sceneLoading.LoadAsync(Ui);
            
            _stateMachine.Value.Enter<GameState>();
        }

        public void Exit()
        {
        }
    }
}