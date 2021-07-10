using CarSumo.Infrastructure.StateMachine;

namespace CarSumo.SceneManagement.SceneStates
{
    public class BootstrapSceneState : IState
    {
        private const string InitialScene = "Initial";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SingleSceneLoader _sceneLoader;

        public BootstrapSceneState(GameStateMachine stateMachine, SingleSceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            _sceneLoader.Load(InitialScene, EnterLoadLevel);
        }

        public void Exit() { }

        private void EnterLoadLevel()
            => _stateMachine.Enter<LoadMenuState>();
    }
}