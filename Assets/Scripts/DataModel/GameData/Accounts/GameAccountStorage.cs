using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using UniRx;

namespace DataModel.GameData.Accounts
{
    public class GameAccountStorage : IAccountStorage, IClientAccountOperations, IClientAccountStorageOperations
    {
        private readonly ReactiveCollection<Account> _allPlayers;
        private readonly ReactiveProperty<Account> _activePlayer;
        
        public GameAccountStorage(Account activePlayer, IEnumerable<Account> allPlayers)
        {
            _activePlayer = new ReactiveProperty<Account>(activePlayer);
            _allPlayers = new ReactiveCollection<Account>(allPlayers);
        }

        public IReadOnlyReactiveCollection<Account> AllPlayers => _allPlayers;

        public IReadOnlyReactiveProperty<Account> ActivePlayer => _activePlayer;

        public void SetActive(Account account)
        {
            _activePlayer.Value = account;
        }

        public bool TryAddAccount(Account account)
        {
            if (_allPlayers.Any(other => other.Name == account.Name))
            {
                return false;
            }
            
            _allPlayers.Add(account);
            return true;
        }
    }
}