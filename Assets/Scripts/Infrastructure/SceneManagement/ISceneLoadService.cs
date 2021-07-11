using System;
using System.Threading.Tasks;

namespace CarSumo.Infrastructure.SceneManagement
{
    public interface ISceneLoadService
    {
        Task Load(SceneLoadData sceneLoadData);
        Task Unload(SceneLoadData sceneLoadData);
    }
}