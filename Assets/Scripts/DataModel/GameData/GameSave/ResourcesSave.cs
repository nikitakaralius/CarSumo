using System;
using System.Collections.Generic;
using CarSumo.DataModel.GameResources;
using DataModel.FileData;

namespace DataModel.GameData.GameSave
{
    public class ResourcesSave : IDisposable
    {
        private readonly IResourceStorage _storage;
        private readonly IResourcesConfiguration _configuration;
        private readonly IFileService _fileService;

        public ResourcesSave(IResourceStorage storage, IResourcesConfiguration configuration, IFileService fileService)
        {
            _storage = storage;
            _configuration = configuration;
            _fileService = fileService;
        }
        
        public void Dispose()
        {
            Save();
        }

        private void Save()
        {
            SerializableResources serializableResources = ToSerializableResources(_storage);
            string path = _configuration.FilePath;
            _fileService.Save(serializableResources, path);
        }

        private SerializableResources ToSerializableResources(IResourceStorage storage)
        {
            int registeredResources = Enum.GetNames(typeof(ResourceId)).Length;
            
            Dictionary<ResourceId, int> amounts = new Dictionary<ResourceId, int>(registeredResources);
            Dictionary<ResourceId, int?> limits = new Dictionary<ResourceId, int?>(registeredResources);

            for (int i = 0; i < registeredResources; i++)
            {
                ResourceId resource = (ResourceId)i;
                
                int amount = storage.GetResourceAmount(resource).Value;
                int? limit = storage.GetResourceLimit(resource).Value;
                
                amounts.Add(resource, amount);
                limits.Add(resource, limit);
            }

            return new SerializableResources()
            {
                Amounts = amounts,
                Limits = limits
            };
        }
    }
}