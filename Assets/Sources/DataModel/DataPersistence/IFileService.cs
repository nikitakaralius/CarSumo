namespace DataModel.DataPersistence
{
    public interface IFileService
    {
        TModel Load<TModel>(string path);
        void Save<TModel>(TModel model, string path);
    }
}