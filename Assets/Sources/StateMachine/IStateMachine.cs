namespace CarSumo.StateMachine
{
    public interface IStateMachine
    {
        void Enter<TState>() where TState : IState;
        void Register(IState state);
    }
}