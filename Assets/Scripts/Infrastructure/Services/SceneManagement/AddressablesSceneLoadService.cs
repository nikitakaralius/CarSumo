using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace CarSumo.Infrastructure.Services.SceneManagement
{
    public class AddressablesSceneLoadService : ISceneLoadService
    {
        private readonly Dictionary<string, SceneInstance> _sceneInstances;

        public AddressablesSceneLoadService()
        {
            _sceneInstances = new Dictionary<string, SceneInstance>();
        }
        
        public async Task Load(SceneLoadData sceneLoadData)
        {
            var asyncOperation = Addressables.LoadSceneAsync(sceneLoadData.Name, sceneLoadData.LoadSceneMode);

            await asyncOperation.Task;
            
            _sceneInstances.Add(sceneLoadData.Name, asyncOperation.Result);
        }

        public async Task Unload(SceneLoadData sceneLoadData)
        {
            SceneInstance sceneToUnload = _sceneInstances[sceneLoadData.Name];
            var asyncOperation = Addressables.UnloadSceneAsync(sceneToUnload);

            await asyncOperation.Task;

            _sceneInstances.Remove(sceneLoadData.Name);
        }
    }
}