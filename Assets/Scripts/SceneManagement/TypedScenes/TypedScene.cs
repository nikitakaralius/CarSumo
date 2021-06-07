using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarSumo.SceneManagement
{
    public abstract class TypedScene
    {
        protected static void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
        {
            void OnLoadCompleted(AsyncOperation asyncOperation)
            {
                var scene = SceneManager.GetSceneByName(sceneName);
                SceneManager.SetActiveScene(scene);
                asyncOperation.completed -= OnLoadCompleted;
            }

            var loader = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            loader.completed += OnLoadCompleted;
        }

        protected static void LoadScene<T>(string sceneName, LoadSceneMode loadSceneMode, T argument)
        {
            void OnLoadCompleted(AsyncOperation asyncOperation)
            {
                var scene = SceneManager.GetSceneByName(sceneName);
                SceneManager.SetActiveScene(scene);
                HandleSceneLoaders(argument);
                asyncOperation.completed -= OnLoadCompleted;
            }

            var loader = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            loader.completed += OnLoadCompleted;
        }

        private static void HandleSceneLoaders<T>(T loadingModel)
        {
            foreach (var rootObjects in SceneManager.GetActiveScene().GetRootGameObjects())
                foreach (var handler in rootObjects.GetComponentsInChildren<ISceneLoadHandler<T>>())
                    handler.OnSceneLoaded(loadingModel);
        }
    }
}