using System;
using System.Collections.Generic;

namespace CarSumo.Infrastructure.StateMachine
{
    public class GameStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine() : this(new Dictionary<Type, IState>()) { }

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

        public void Register<TState>(TState state) where TState : IState
        {
            Type stateKey = state.GetType();
            _states.Add(stateKey, state);
        }

        public void Register(IEnumerable<IState> states)
        {
            foreach (IState state in states)
            {
                Register(state);
            }
        }
    }
}