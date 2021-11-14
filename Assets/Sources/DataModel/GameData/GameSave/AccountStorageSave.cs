using System;
using System.Linq;
using CarSumo.DataModel.Accounts;
using CarSumo.DataModel.GameData.Accounts;
using DataModel.DataPersistence;
using UniRx;

namespace DataModel.GameData.GameSave
{
    public class AccountStorageSave : IDisposable
    {
        private readonly IAsyncFileService _fileService;
        private readonly IAccountStorage _accountStorage;
        private readonly IAccountStorageMessages _storageMessages;
        private readonly IAccountStorageConfiguration _configuration;
        private readonly IAccountSerialization _accountSerialization;

        private IDisposable _layoutChangedSubscription;
        
        public AccountStorageSave(IAsyncFileService fileService,
                                IAccountStorage accountStorage,
                                IAccountStorageMessages storageMessages,
                                IAccountStorageConfiguration configuration,
                                IAccountSerialization accountSerialization)
        {
            _fileService = fileService;
            _accountStorage = accountStorage;
            _configuration = configuration;
            _accountSerialization = accountSerialization;
            _storageMessages = storageMessages;

            accountStorage.ActiveAccount.Subscribe(OnActiveAccountChanged);
            accountStorage.AllAccounts.ObserveCountChanged().Subscribe(_ => Save());
            accountStorage.AllAccounts.ObserveReplace().Subscribe(_ => Save());
            _storageMessages.ObserveAnyAccountValueChanged().Subscribe(_ => Save());
        }
        
        public void Dispose()
        {
            Save();
        }

        private void Save()
        {
            SerializableAccountStorage storage = ToSerializableAccountStorage(_accountStorage);
            string filePath = _configuration.AccountStorageFilePath;
            _fileService.SaveAsync(storage, filePath);
        }

        private SerializableAccountStorage ToSerializableAccountStorage(IAccountStorage storage)
        {
            return new SerializableAccountStorage()
            {
                ActiveAccount = _accountSerialization.SerializeFrom(storage.ActiveAccount.Value),
                AllAccounts = storage.AllAccounts.Select(player => _accountSerialization.SerializeFrom(player))
            };
        }
        
        private void OnActiveAccountChanged(Account account)
        {
	        _layoutChangedSubscription?.Dispose();
            
	        Save();
	        
	        _layoutChangedSubscription = account
		        .VehicleLayout.ObserveLayoutCompletedChanging()
		        .Subscribe(_ => Save());
        }
    }
}