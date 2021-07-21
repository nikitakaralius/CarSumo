using System.Threading.Tasks;
using CarSumo.Infrastructure.Services.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace CarSumo.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private const string Menu = "Menu";
        private const string PlayersAssets = "Players";
        private readonly ISceneLoadService _sceneLoadService;

        public BootstrapState(ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
        }

        public async void Enter()
        {
            var sceneLoadData = new SceneLoadData(Menu, LoadSceneMode.Single);
            await PreloadAssets();
            await _sceneLoadService.Load(sceneLoadData);
        }

        public void Exit() { }

        private async Task PreloadAssets()
        {
            await Addressables.LoadAssetAsync<Sprite>(PlayersAssets).Task;
        }
    }
}