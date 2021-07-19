using System.Threading.Tasks;

namespace CarSumo.DataManagement.Core
{
    public interface IDataSave
    {
        void Save<TModel>(TModel model, string path);
        Task SaveAsync<TModel>(TModel model, string path);
    }
}