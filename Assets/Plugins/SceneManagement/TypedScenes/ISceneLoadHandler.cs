namespace CarSumo.SceneManagement
{
    public interface ISceneLoadHandler<in T>
    {
        void OnSceneLoaded(T argument);
    }
}