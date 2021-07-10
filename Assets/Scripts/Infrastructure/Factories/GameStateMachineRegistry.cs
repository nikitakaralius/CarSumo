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
        private readonly SingleSceneLoader _singleSceneLoader;

        public GameStateMachineRegistry(SingleSceneLoader singleSceneLoader)
        {
            _singleSceneLoader = singleSceneLoader;
        }
        
        public GameStateMachine Create()
        {
            GameStateMachine stateMachine = new GameStateMachine();
            IEnumerable<IState> states = CreateStatesRegistry(stateMachine);
            stateMachine.Register(states);
            return stateMachine;
        }

        private IEnumerable<IState> CreateStatesRegistry(GameStateMachine stateMachine)
        {
            return new IState[]
            {
                new BootstrapSceneState(stateMachine, _singleSceneLoader),
                new LoadMenuState(_singleSceneLoader)
            };
        }
    }
}