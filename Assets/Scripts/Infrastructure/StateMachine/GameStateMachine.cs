using System;
using System.Collections.Generic;

namespace CarSumo.Infrastructure.StateMachine
{
    public class GameStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(Dictionary<Type, IState> states)
        {
            _states = states;
            _activeState = new EmptyState();
        }
        
        public void Enter<TState>() where TState : IState
        {
            _activeState.Exit();
            _activeState = _states[typeof(TState)];
            _activeState.Enter();
        }
    }
}