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
        private readonly ProjectInitialization _projectInitialization;

        public GameStateMachineRegistry(IAsyncSceneLoading sceneLoading, ProjectInitialization projectInitialization)
        {
            _sceneLoading = sceneLoading;
            _projectInitialization = projectInitialization;
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
            stateMachine.RegisterState(new BootstrapState(stateMachine, _projectInitialization));
            stateMachine.RegisterState(new GameEntryState(_sceneLoading, stateMachine));
        }
    }
}