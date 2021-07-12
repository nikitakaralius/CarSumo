using UnityEditor.VersionControl;

namespace CarSumo.Infrastructure.Services.LoadingScreen
{
    public interface ILoadingScreen
    {
        void Enable();
        void Disable();
    }
}