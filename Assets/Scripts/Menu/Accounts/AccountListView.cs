using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using Services.Instantiate;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Menu.Accounts
{
    public class AccountListView : MonoBehaviour
    {
        [SerializeField] private AssetReferenceGameObject _accountViewPrefab;
        [SerializeField] private AssetReferenceGameObject _blankAccountViewPrefab;
        [SerializeField] private Transform _itemsRoot;

        private IAsyncInstantiation _instantiation;
        private IAccountStorage _accountStorage;

        [Inject]
        private void Construct(IAsyncInstantiation instantiation, IAccountStorage accountStorage)
        {
            _instantiation = instantiation;
            _accountStorage = accountStorage;
        }

        private async void Start()
        {
                      
        }
        
        private IEnumerable<Account> GetAccountsWithoutActive(IEnumerable<Account> allAccounts, Account activeAccount)
        {
            return allAccounts.Where(account => account.Name != activeAccount.Name);
        }
    }
}