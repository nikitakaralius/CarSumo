using System.Threading.Tasks;

namespace CarSumo.Infrastructure.Services.LoadingScreen
{
    public interface ILoadingScreen
    {
        Task Enable();
        Task Disable();
    }
}