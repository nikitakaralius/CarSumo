using System.Collections.Generic;
using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using Services.SceneManagement;
using Zenject;

namespace Infrastructure.Installers.Factories
{
    public class GameStateMachineRegistry : IFactory<GameStateMachine>
    {
        private readonly IAsyncSceneLoading _sceneLoading;

        public GameStateMachineRegistry(IAsyncSceneLoading sceneLoading)
        {
            _sceneLoading = sceneLoading;
        }

        public GameStateMachine Create()
        {
            GameStateMachine stateMachine = new GameStateMachine(RegisterResolvedStates());
            RegisterStateMachineDependentStates(stateMachine);
            return stateMachine;
        }

        private IEnumerable<IState> RegisterResolvedStates()
        {
            return new IState[]
            {
                new MenuEntryState(_sceneLoading),
                new GameState(),
                new PauseState()
            };
        }

        private void RegisterStateMachineDependentStates(GameStateMachine stateMachine)
        {
            stateMachine.RegisterState(new BootstrapState(stateMachine));
            stateMachine.RegisterState(new GameEntryState(_sceneLoading, stateMachine));
        }
    }
}