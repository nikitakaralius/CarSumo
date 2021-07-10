﻿namespace CarSumo.Infrastructure.StateMachine
{
    public interface IStateMachine
    {
        void Enter<TState>() where TState : IState;
    }
}