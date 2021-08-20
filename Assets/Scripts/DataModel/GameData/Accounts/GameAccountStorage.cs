using System;
using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using UniRx;

namespace DataModel.GameData.Accounts
{
    public class GameAccountStorage : IAccountStorage,
									IAccountStorageMessages,
	    							IClientAccountOperations,
	    							IClientAccountStorageOperations,
	    							IServerAccountOperations
    {
        private readonly ReactiveCollection<Account> _allAccounts;
        private readonly ReactiveProperty<Account> _activeAccount;
        private readonly Subject<Account> _anyAccountChangedObserver;

        public GameAccountStorage(Account activeAccount, IEnumerable<Account> allAccounts)
        {
            _activeAccount = new ReactiveProperty<Account>(activeAccount);
            _allAccounts = new ReactiveCollection<Account>(allAccounts);

            _anyAccountChangedObserver = new Subject<Account>();
        }

        public IReadOnlyReactiveCollection<Account> AllAccounts => _allAccounts;

        public IReadOnlyReactiveProperty<Account> ActiveAccount => _activeAccount;

        public IObservable<Account> ObserveAnyAccountValueChanged()
        {
	        return _anyAccountChangedObserver;
        }

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

        public bool TryChangeName(Account account, string newName)
        {
	        if (ContainsAccountWithTheSameName(account, newName))
		        return false;

	        account.Name.Value = newName;
	        _anyAccountChangedObserver.OnNext(account);
	        return true;
        }

        public void ChangeIcon(Account account, Icon icon)
        {
	        account.Icon.Value = icon;
	        _anyAccountChangedObserver.OnNext(account);
        }

        public bool TryRemove(Account account)
        {
	        if (_allAccounts.Count == 1)
		        return false;
	        
	        if (_activeAccount.Value.Equals(account))
		        _activeAccount.Value = _allAccounts.First(x => x.Equals(account) == false);
	        
	        _allAccounts.Remove(account);

	        return true;
        }

        private bool ContainsAccountWithTheSameName(Account accountToChange, string newName)
        {
	        foreach (Account account in _allAccounts)
	        {
		        if (accountToChange.Equals(account))
			        continue;

		        if (account.Name.Value == newName)
			        return true;
	        }

	        return false;
        }
    }
}