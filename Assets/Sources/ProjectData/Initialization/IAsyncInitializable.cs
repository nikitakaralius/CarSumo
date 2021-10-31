using System.Threading.Tasks;

namespace Infrastructure.Initialization
{
    public interface IAsyncInitializable
    {
        Task InitializeAsync();
    }
}