using System;
using System.Linq;
using CarSumo.DataModel.Accounts;
using CarSumo.DataModel.GameData.Accounts;
using DataModel.FileData;

namespace DataModel.GameData.GameSave
{
    public class GameAccountsSave : IDisposable
    {
        private readonly IFileService _fileService;
        private readonly IAccountStorage _accountStorage;
        private readonly IAccountStorageConfiguration _configuration;
        private readonly IAccountSerialization _accountSerialization;

        public GameAccountsSave(IFileService fileService,
                                IAccountStorage accountStorage,
                                IAccountStorageConfiguration configuration,
                                IAccountSerialization accountSerialization)
        {
            _fileService = fileService;
            _accountStorage = accountStorage;
            _configuration = configuration;
            _accountSerialization = accountSerialization;
        }
        
        public void Dispose()
        {
            SerializableAccountStorage storage = ToSerializableAccountStorage(_accountStorage);
            string filePath = _configuration.FilePath;
            _fileService.Save(storage, filePath);
        }

        private SerializableAccountStorage ToSerializableAccountStorage(IAccountStorage storage)
        {
            return new SerializableAccountStorage()
            {
                ActiveAccount = _accountSerialization.SerializeFrom(storage.ActivePlayer.Value),
                AllPlayers = storage.AllPlayers.Select(player => _accountSerialization.SerializeFrom(player))
            };
        }
    }
}