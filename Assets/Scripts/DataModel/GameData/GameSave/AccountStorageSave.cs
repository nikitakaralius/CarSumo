﻿using System;
using System.Linq;
using CarSumo.DataModel.Accounts;
using CarSumo.DataModel.GameData.Accounts;
using DataModel.FileData;

namespace DataModel.GameData.GameSave
{
    public class AccountStorageSave : IDisposable
    {
        private readonly IFileService _fileService;
        private readonly IAccountStorage _accountStorage;
        private readonly IAccountStorageConfiguration _configuration;
        private readonly IAccountSerialization _accountSerialization;

        public AccountStorageSave(IFileService fileService,
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
            Save();
        }

        private void Save()
        {
            SerializableAccountStorage storage = ToSerializableAccountStorage(_accountStorage);
            string filePath = _configuration.AccountStorageFilePath;
            _fileService.Save(storage, filePath);
        }

        private SerializableAccountStorage ToSerializableAccountStorage(IAccountStorage storage)
        {
            return new SerializableAccountStorage()
            {
                ActiveAccount = _accountSerialization.SerializeFrom(storage.ActiveAccount.Value),
                AllAccounts = storage.AllAccount.Select(player => _accountSerialization.SerializeFrom(player))
            };
        }
    }
}