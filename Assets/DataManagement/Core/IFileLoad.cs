using System.Threading.Tasks;

namespace CarSumo.DataManagement.Core
{
    public interface IFileLoad
    {
        TModel Load<TModel>(string path);
        Task<TModel> LoadAsync<TModel>(string path);
    }
}