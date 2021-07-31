using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;
using CarSumo.DataModel.GameData.Accounts;
using DataModel.FileData;
using DataModel.GameData.Accounts.Extensions;
using UniRx;
using Zenject;

namespace DataModel.GameData.Accounts
{
    public class GameAccountStorage : IAccountStorage, IClientAccountOperations, IClientAccountStorageOperations, IInitializable
    {
        private readonly IAsyncFileService _fileService;
        private readonly IAccountStorageConfiguration _configuration;
        private readonly IAsyncAccountBinding _accountBinding;

        private ReactiveCollection<Account> _allPlayers;
        private ReactiveProperty<Account> _activePlayer;
        
        public GameAccountStorage(IAsyncFileService fileService, IAccountStorageConfiguration configuration, IAsyncAccountBinding accountBinding)
        {
            _fileService = fileService;
            _configuration = configuration;
            _accountBinding = accountBinding;
        }

        public IReadOnlyReactiveCollection<Account> AllPlayers => _allPlayers;

        public IReadOnlyReactiveProperty<Account> ActivePlayer => _activePlayer;

        public async void Initialize()
        {
            SerializableAccountStorage storage = await 
                _fileService.LoadAsync<SerializableAccountStorage>(_configuration.FilePath);

            IEnumerable<Account> boundAccounts = await storage.AllPlayers.ToBoundAccountsAsync(_accountBinding);
            Account activeAccount = await _accountBinding.ToAccountAsync(storage.ActiveAccount);

            _allPlayers = new ReactiveCollection<Account>(boundAccounts);
            _activePlayer = new ReactiveProperty<Account>(activeAccount);
        }

        public void SetActive(Account account)
        {
            _activePlayer.Value = account;
            SaveAsync();
        }

        public bool TryAddAccount(Account account)
        {
            if (_allPlayers.Any(other => other.Name == account.Name))
            {
                return false;
            }
            _allPlayers.Add(account);
            SaveAsync();
            return true;
        }

        private async void SaveAsync()
        {
            SerializableAccountStorage storage = await ToSerializableAccountStorageAsync(this);
            await _fileService.SaveAsync(storage, _configuration.FilePath);
        }

        private async Task<SerializableAccountStorage> ToSerializableAccountStorageAsync(IAccountStorage storage)
        {
            return new SerializableAccountStorage()
            {
                ActiveAccount = await _accountBinding.ToSerializableAccountAsync(storage.ActivePlayer.Value),
                AllPlayers = await storage.AllPlayers.ToSerializableAccountsAsync(_accountBinding)
            };
        }
    }
}