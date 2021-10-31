using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataModel.FileData
{
    public class JsonNetFileService : IFileService, IAsyncFileService
    {
        public TModel Load<TModel>(string path)
        {
            using var reader = new StreamReader(path);
            using var jsonReader = new JsonTextReader(reader);
            
            var serializer = new JsonSerializer();

            TModel model = serializer.Deserialize<TModel>(jsonReader);
            return model;
        }

        public void Save<TModel>(TModel model, string path)
        {
            using var writer = new StreamWriter(path, false);
            var serializer = new JsonSerializer();
            serializer.Serialize(writer, model);
        }

        public async Task<TModel> LoadAsync<TModel>(string path)
        {
            using var reader = new StreamReader(path);
            
            string json = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<TModel>(json);
        }

        public async Task SaveAsync<TModel>(TModel model, string path)
        {
            await Task.Run(() => Save(model, path));
        }
    }
}