using System;
using UnityEngine.SceneManagement;

namespace CarSumo.SceneManagement
{
    public class SingleSceneLoader : SceneLoader
    {
        public override void Load(string sceneName, Action loaded)
        {
            Load(sceneName, loaded, LoadSceneMode.Single);
        }

        public override void Unload(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}