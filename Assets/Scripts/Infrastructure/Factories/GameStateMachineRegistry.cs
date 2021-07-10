using System;
using System.Collections.Generic;
using CarSumo.Infrastructure.StateMachine;
using CarSumo.SceneManagement;
using CarSumo.SceneManagement.SceneStates;
using Zenject;

namespace CarSumo.Infrastructure.Factories
{
    public class GameStateMachineRegistry : IFactory<GameStateMachine>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SingleSceneLoader _singleSceneLoader;
        private readonly AdditiveSceneLoader _additiveSceneLoader;

        public GameStateMachineRegistry(GameStateMachine stateMachine,
                                        SingleSceneLoader singleSceneLoader,
                                        AdditiveSceneLoader additiveSceneLoader)
        {
            _stateMachine = stateMachine;
            _singleSceneLoader = singleSceneLoader;
            _additiveSceneLoader = additiveSceneLoader;
        }
        
        public GameStateMachine Create()
        {
            return new GameStateMachine(RegistryStates());
        }

        private Dictionary<Type, IState> RegistryStates()
        {
            return new Dictionary<Type, IState>
            {
                { typeof(BootstrapSceneState), new BootstrapSceneState(_stateMachine, _singleSceneLoader) },
                { typeof(LoadMenuState), new LoadMenuState(_singleSceneLoader)}
            };
        }
    }
}