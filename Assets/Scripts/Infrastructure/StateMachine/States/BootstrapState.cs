using CarSumo.Infrastructure.Services.SceneManagement;
using UnityEngine.SceneManagement;

namespace CarSumo.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private const string Menu = "Menu";
        private readonly ISceneLoadService _sceneLoadService;

        public BootstrapState(ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
        }

        public void Enter()
        {
            var sceneLoadData = new SceneLoadData(Menu, LoadSceneMode.Single);
            _sceneLoadService.Load(sceneLoadData);
        }

        public void Exit() { }
    }
}