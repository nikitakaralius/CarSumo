using Infrastructure;

namespace CarSumo.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ProjectInitialization _projectInitialization;

        public BootstrapState(GameStateMachine stateMachine, ProjectInitialization projectInitialization)
        {
            _stateMachine = stateMachine;
            _projectInitialization = projectInitialization;
        }
        
        public async void Enter()
        {
            await _projectInitialization.InitializeAsync();
            _stateMachine.Enter<MenuEntryState>();
        }

        public void Exit()
        {
            
        }
    }
}