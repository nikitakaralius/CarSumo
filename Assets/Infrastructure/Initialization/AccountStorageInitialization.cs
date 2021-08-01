using System.Collections.Generic;
using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;
using CarSumo.DataModel.GameData.Accounts;
using DataModel.FileData;
using DataModel.GameData.Accounts;
using DataModel.GameData.GameSave;
using Zenject;

namespace Infrastructure.Initialization
{
    public class AccountStorageInitialization
    {
        private readonly DiContainer _container;
        private readonly IAccountStorageConfiguration _configuration;
        private readonly IAsyncAccountBinding _accountBinding;
        private readonly IAsyncFileService _fileService;

        public AccountStorageInitialization(DiContainer container,
            IAccountStorageConfiguration configuration,
            IAsyncAccountBinding accountBinding,
            IAsyncFileService fileService)
        {
            _container = container;
            _configuration = configuration;
            _accountBinding = accountBinding;
            _fileService = fileService;
        }

        public async Task InitializeAsync()
        {
            SerializableAccountStorage serializableStorage = await LoadSerializableAccountStorage() ?? EnsureCreated();
            GameAccountStorage storage = await InitializeGameAccountStorage(serializableStorage);

            BindStorageInterfaces(storage);
            BindAccountStorageSave();
        }

        private SerializableAccountStorage EnsureCreated()
        {
            SerializableAccount activeAccount = new UnregisteredSerializableAccount();

            return new SerializableAccountStorage()
            {
                ActiveAccount = activeAccount,
                AllAccounts = new[] {activeAccount}
            };
        }

        private void BindAccountStorageSave()
        {
            _container
                .Bind<AccountStorageSave>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindStorageInterfaces(GameAccountStorage storage)
        {
            _container
                .BindInterfacesAndSelfTo<GameAccountStorage>()
                .FromInstance(storage)
                .AsSingle()
                .NonLazy();
        }

        private async Task<GameAccountStorage> InitializeGameAccountStorage(SerializableAccountStorage storage)
        {
            Account activeAccount = await _accountBinding.ToAccountAsync(storage.ActiveAccount);
            IEnumerable<Account> allAccounts = await GetBoundAccounts(storage);
            return new GameAccountStorage(activeAccount, allAccounts);
        }

        private async Task<IEnumerable<Account>> GetBoundAccounts(SerializableAccountStorage storage)
        {
            var accounts = new List<Account>();
            foreach (SerializableAccount serializableAccount in storage.AllAccounts)
            {
                Account account = await _accountBinding.ToAccountAsync(serializableAccount);
                accounts.Add(account);
            }

            return accounts;
        }

        private async Task<SerializableAccountStorage> LoadSerializableAccountStorage()
        {
            string filePath = _configuration.AccountStorageFilePath;
            return await _fileService.LoadAsync<SerializableAccountStorage>(filePath);
        }
    }
}