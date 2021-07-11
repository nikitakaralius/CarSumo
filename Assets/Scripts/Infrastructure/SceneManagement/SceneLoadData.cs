using UnityEngine.SceneManagement;

namespace CarSumo.Infrastructure.SceneManagement
{
    public readonly struct SceneLoadData
    {
        public SceneLoadData(string name, LoadSceneMode loadSceneMode)
        {
            Name = name;
            LoadSceneMode = loadSceneMode;
        }
        
        public string Name { get; }
        
        public LoadSceneMode LoadSceneMode { get; }
    }
}