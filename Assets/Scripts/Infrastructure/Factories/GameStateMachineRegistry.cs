using System.Collections.Generic;
using CarSumo.Infrastructure.Services.LoadingScreen;
using CarSumo.Infrastructure.Services.SceneManagement;
using CarSumo.Infrastructure.StateMachine;
using CarSumo.Infrastructure.StateMachine.States;
using Zenject;

namespace CarSumo.Infrastructure.Factories
{
    public class GameStateMachineRegistry : IFactory<GameStateMachine>
    {
        private readonly ISceneLoadService _sceneLoadService;
        private readonly ILoadingScreen _loadingScreen;

        public GameStateMachineRegistry(ISceneLoadService sceneLoadService, ILoadingScreen loadingScreen)
        {
            _sceneLoadService = sceneLoadService;
            _loadingScreen = loadingScreen;
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
                new GameEntryState(_sceneLoadService, _loadingScreen)
            };
        }
    }
}