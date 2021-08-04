using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using UniRx;

namespace DataModel.GameData.Accounts
{
    public class GameAccountStorage : IAccountStorage, IClientAccountOperations, IClientAccountStorageOperations
    {
        private readonly ReactiveCollection<Account> _allAccounts;
        private readonly ReactiveProperty<Account> _activeAccount;
        
        public GameAccountStorage(Account activeAccount, IEnumerable<Account> allAccounts)
        {
            _activeAccount = new ReactiveProperty<Account>(activeAccount);
            _allAccounts = new ReactiveCollection<Account>(allAccounts);
        }

        public IReadOnlyReactiveCollection<Account> AllAccounts => _allAccounts;

        public IReadOnlyReactiveProperty<Account> ActiveAccount => _activeAccount;

        public void SetActive(Account account)
        {
            _activeAccount.Value = account;
        }

        public bool TryAddAccount(Account account)
        {
            if (account.Name.Value == null)
                return false;
            if (account.Name.Value.Length == 0)
                return false;
            if (_allAccounts.Any(account.Equals))
                return false;

            _allAccounts.Add(account);
            return true;
        }
    }
}