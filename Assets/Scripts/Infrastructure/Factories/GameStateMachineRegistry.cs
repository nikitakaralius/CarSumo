using System.Collections.Generic;
using CarSumo.Infrastructure.SceneManagement;
using CarSumo.Infrastructure.StateMachine;
using CarSumo.Infrastructure.StateMachine.States;
using Zenject;

namespace CarSumo.Infrastructure.Factories
{
    public class GameStateMachineRegistry : IFactory<GameStateMachine>
    {
        private readonly ISceneLoadService _sceneLoadService;

        public GameStateMachineRegistry(ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
        }

        public GameStateMachine Create()
        {
            return new GameStateMachine(RegisteredStates());
        }

        private IEnumerable<IState> RegisteredStates()
        {
            return new IState[]
            {
                new BootstrapState(_sceneLoadService),
                new GameEntryState(_sceneLoadService)
            };
        }
    }
}