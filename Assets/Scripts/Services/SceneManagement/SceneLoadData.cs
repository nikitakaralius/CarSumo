using UnityEngine.SceneManagement;

namespace Services.SceneManagement
{
    public readonly struct SceneLoadData
    {
        public string Name { get; }
        public LoadSceneMode LoadSceneMode { get; }

        public SceneLoadData(string name, LoadSceneMode loadSceneMode)
        {
            Name = name;
            LoadSceneMode = loadSceneMode;
        }
    }
}