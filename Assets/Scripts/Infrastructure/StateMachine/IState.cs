namespace CarSumo.Infrastructure.StateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
    }

    public class EmptyState : IState
    {
        public void Enter() { }
        public void Exit() { }
    }
}