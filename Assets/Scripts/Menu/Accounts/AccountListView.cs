using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;
using CarSumo.DataModel.GameResources;
using Services.Instantiate;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace Menu.Accounts
{
    public class AccountListView : MonoBehaviour
    {
        [SerializeField] private AssetReferenceGameObject _accountViewPrefab;
        [SerializeField] private AssetReferenceGameObject _blankAccountViewPrefab;
        [SerializeField] private Transform _itemsRoot;
        [SerializeField] private LayoutGroup _layoutGroup;

        private IAsyncInstantiation _instantiation;
        private IAccountStorage _accountStorage;
        private IResourceStorage _resourceStorage;
        private IDisposable _subscription;

        private readonly List<GameObject> _views = new List<GameObject>();

        [Inject]
        private void Construct(IAsyncInstantiation instantiation,
            IAccountStorage accountStorage,
            IResourceStorage resourceStorage)
        {
            _instantiation = instantiation;
            _accountStorage = accountStorage;
            _resourceStorage = resourceStorage;
        }

        private async void Awake()
        {
            await FillList();

            _subscription = _accountStorage.AllAccounts
                .ObserveCountChanged()
                .Subscribe(async _ => await FillList());
        }

        private void OnDestroy()
        {
            _subscription.Dispose();
        }

        private async Task FillList()
        {
            ClearViews();
            
            IEnumerable<GameObject> accountViews = await CreateAccountViews(_accountStorage.AllAccounts, _itemsRoot);
            IEnumerable<GameObject> blankAccountViews = await CreateBlankAccountViews(_itemsRoot);

            _views.AddRange(accountViews);
            _views.AddRange(blankAccountViews);
        }

        private void ClearViews()
        {
            foreach (GameObject view in _views)
            {
                Destroy(view);
            } 
            _views.Clear();
        }

        private async Task<IEnumerable<GameObject>> CreateAccountViews(IEnumerable<Account> accounts, Transform root)
        {
            if (AreAccountsFitIntoLimit(_resourceStorage, _accountStorage) == false)
            {
                throw new InvalidOperationException("The allowed number of accounts has been exceeded");
            }

            var views = new List<GameObject>();
            
            foreach (Account account in accounts)
            {
                AccountListItem listItem =
                    await _instantiation.InstantiateAsync<AccountListItem>(_accountViewPrefab, root);
                listItem.Initialize(account, _itemsRoot, transform, _layoutGroup);
                views.Add(listItem.gameObject);
            }

            return views;
        }

        private async Task<IEnumerable<GameObject>> CreateBlankAccountViews(Transform root)
        {
            int blanksCount = CountBlankAccounts(_resourceStorage, _accountStorage);
            var views = new GameObject[blanksCount];
            
            for (int i = 0; i < blanksCount; i++)
            {
                var view = await _instantiation.InstantiateAsync<BlankAccountListView>(_blankAccountViewPrefab, root);
                views[i] = view.gameObject;
            }

            return views;
        }

        private int CountBlankAccounts(IResourceStorage resourceStorage, IAccountStorage accountStorage)
        {
            IReadOnlyReactiveProperty<int?> slotsLimit = resourceStorage.GetResourceLimit(ResourceId.AccountSlots);

            if (slotsLimit.HasValue == false)
            {
                throw new InvalidOperationException("Slots limit must be determined");
            }

            return slotsLimit.Value.Value - accountStorage.AllAccounts.Count;
        }

        private bool AreAccountsFitIntoLimit(IResourceStorage resourceStorage, IAccountStorage accountStorage)
        {
            IReadOnlyReactiveProperty<int?> slotsLimit = resourceStorage.GetResourceLimit(ResourceId.AccountSlots);

            if (slotsLimit.HasValue == false)
            {
                throw new InvalidOperationException("Slots limit must be determined");
            }

            return slotsLimit.Value >= accountStorage.AllAccounts.Count;
        }
    }
}