using CarSumo.Infrastructure.StateMachine;

namespace CarSumo.SceneManagement.SceneStates
{
    public class LoadMenuState : IState
    {
        private const string MenuScene = "Menu";
        private readonly SingleSceneLoader _sceneLoader;

        public LoadMenuState(SingleSceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            _sceneLoader.Load(MenuScene, null);
        }

        public void Exit() { }
    }
}