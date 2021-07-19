using System.IO;
using System.Threading.Tasks;
using CarSumo.DataManagement.Core;
using Newtonsoft.Json;

namespace DataManagement.Services
{
    public class JsonDataService : IDataService
    {
        public TModel Load<TModel>(string path)
        {
            string json = File.ReadAllText(path);
            TModel model = JsonConvert.DeserializeObject<TModel>(json);

            return model;
        }

        public async Task<TModel> LoadAsync<TModel>(string path)
        {
            using var reader = new StreamReader(path);

            Task<string> readToEnd = reader.ReadToEndAsync();
            await readToEnd;

            string json = readToEnd.Result;
            return await Task<TModel>.Run(() => JsonConvert.DeserializeObject<TModel>(json));
        }

        public void Save<TModel>(TModel model, string path)
        {
            using var writer = new StreamWriter(path, false);
            
            var serializer = new JsonSerializer();
            serializer.Serialize(writer, model);
        }

        public async Task SaveAsync<TModel>(TModel model, string path)
        {
            await Task.Run(() => Save(model, path));
        }
    }
}