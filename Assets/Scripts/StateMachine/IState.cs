namespace CarSumo.StateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();

        public class Empty : IState
        {
            public void Enter() { }
            public void Exit() { }
        }
    }
}