using System;
using UnityEngine.SceneManagement;

namespace CarSumo.SceneManagement
{
    public class AdditiveSceneLoader : SceneLoader
    {
        public override void Load(string sceneName, Action loaded)
        {
            Load(sceneName, loaded, LoadSceneMode.Additive);
        }

        public override void Unload(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}