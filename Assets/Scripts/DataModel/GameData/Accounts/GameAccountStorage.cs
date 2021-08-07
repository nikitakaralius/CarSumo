using System;
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

        public void ChangeOrder(IReadOnlyList<Account> order)
        {
            if (order.Count != _allAccounts.Count)
            {
                throw new InvalidOperationException("Trying to change order with different count");
            }

            IEnumerable<Account> cachedAccounts = _allAccounts.ToArray();

            for (var i = 0; i < _allAccounts.Count; i++)
            {
                if (cachedAccounts.Any(account => account.Equals(order[i])))
                {
                    _allAccounts[i] = order[i];
                }
                else
                {
                    throw new InvalidOperationException("Trying to change order with non-existing account");
                }
            }
        }
    }
}