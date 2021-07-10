using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarSumo.SceneManagement
{
    public abstract class SceneLoader
    {
        public abstract void Load(string sceneName, Action loaded);
        
        public abstract void Unload(string sceneName);

        protected void Load(string sceneName, Action loaded, LoadSceneMode loadSceneMode)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            operation.completed += asyncOperation => loaded?.Invoke();
        }
    }
}