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
            return new GameStateMachine(RegisterStates());
        }

        private IEnumerable<IState> RegisterStates()
        {
            return new IState[]
            {
                new BootstrapState(_sceneLoading),
                new GameEntryState(_sceneLoading)
            };
        }
    }
}