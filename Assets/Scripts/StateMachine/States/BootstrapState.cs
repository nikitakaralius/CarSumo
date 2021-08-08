using Infrastructure;
using Zenject;

namespace CarSumo.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly ProjectInitialization _projectInitialization;
        private readonly LazyInject<GameStateMachine> _stateMachine;

        public BootstrapState(ProjectInitialization projectInitialization, LazyInject<GameStateMachine> stateMachine)
        {
            _projectInitialization = projectInitialization;
            _stateMachine = stateMachine;
        }

        public async void Enter()
        {
            await _projectInitialization.InitializeAsync();
            _stateMachine.Value.Enter<MenuEntryState>();
        }

        public void Exit()
        {
            
        }
    }
}