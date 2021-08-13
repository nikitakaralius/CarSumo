using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;
using CarSumo.DataModel.GameResources;
using JetBrains.Annotations;
using Menu.Buttons;
using Services.Instantiate;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Menu.Accounts
{
	public class AccountListView : MonoBehaviour
    {
	    [Header("Account Views")]
	    [SerializeField] private AssetReferenceGameObject _accountViewPrefab;
	    [SerializeField] private AssetReferenceGameObject _blankAccountViewPrefab;
	    
	    [Header("List Preferences")]
	    [SerializeField] private Transform _itemsRoot;
	    [SerializeField] private ManageableAccountList _defaultBehaviour;

	    private IAccountListRules _rules;
	    private IButtonSelectHandler<AccountListItem> _itemSelectHandler;
	    private IAccountListObserver _observer;
	    
	    private IAsyncInstantiation _instantiation;
	    private IAccountStorage _accountStorage;
	    private IResourceStorage _resourceStorage;
	    
	    private IDisposable _accountsChangedSubscription;

	    private readonly List<AccountListItem> _items = new List<AccountListItem>();
	    private readonly List<GameObject> _allViews = new List<GameObject>();
	    
	    [Inject]
	    private void Construct(IAsyncInstantiation instantiation, IAccountStorage accountStorage, IResourceStorage resourceStorage)
	    {
		    _instantiation = instantiation;
		    _accountStorage = accountStorage;
		    _resourceStorage = resourceStorage;
	    }


	    private void Awake()
	    {
		    _accountsChangedSubscription = _accountStorage.AllAccounts
			    .ObserveCountChanged()
			    .Subscribe(_ => FillList());
	    }
	    
	    private void OnDestroy()
	    {
		    _accountsChangedSubscription?.Dispose();
	    }

	    public void OpenInternal()
	    {
		    Open(_defaultBehaviour, _defaultBehaviour, _defaultBehaviour);
	    }

	    public void Open(IAccountListRules rules,
		    			 IButtonSelectHandler<AccountListItem> selectHandler,
		    			 [CanBeNull] IAccountListObserver observer = null)
	    {
		    _rules = rules;
		    _itemSelectHandler = selectHandler;
		    _observer = observer;
		    
		    gameObject.SetActive(true);
		    
		    FillList();
	    }

	    public void Close()
	    {
		    gameObject.SetActive(false);
	    }

	    private async void FillList()
	    {
		    ClearPrevious();

		    IEnumerable<AccountListItem> accountListItems = await CreateAccountListItems(_rules.AccountsToRender, _itemsRoot);
		    IEnumerable<GameObject> blankAccountViews = await CreateBlankAccountViews(_itemsRoot);

		    IEnumerable<GameObject> accountListViews = accountListItems.Select(item => item.gameObject);

		    _allViews.AddRange(accountListViews);
		    _allViews.AddRange(blankAccountViews);
		    
		    _items.AddRange(accountListItems);
		    
		    _observer?.OnAllItemsCreated(_items);
	    }
	    
	    private void ClearPrevious()
	    {
		    foreach (GameObject view in _allViews)
		    {
			    Destroy(view);
		    }
		    
		    _allViews.Clear();
		    _items.Clear();
	    }

	    private async Task<IEnumerable<AccountListItem>> CreateAccountListItems(IEnumerable<Account> accounts, Transform root)
	    {
		    if (AreAccountsFitIntoLimit(_resourceStorage, _accountStorage) == false)
		    {
			    throw new InvalidOperationException("The allowed number of accounts has been exceeded");
		    }

		    var views = new List<AccountListItem>();
            
		    foreach (Account account in accounts)
		    {
			    AccountListItem listItem = await _instantiation.InstantiateAsync<AccountListItem>(_accountViewPrefab, root);
			    listItem.Initialize(account, _itemSelectHandler);
			    views.Add(listItem);
			    
			    _observer?.OnAccountCreated(listItem);
		    }

		    return views;
	    }

	    private async Task<IEnumerable<GameObject>> CreateBlankAccountViews(Transform root)
	    {
		    int blanksCount = CountBlankAccounts(_resourceStorage, _accountStorage);
		    var views = new GameObject[blanksCount];
            
		    for (int i = 0; i < blanksCount; i++)
		    {
			    var view = await _instantiation.InstantiateAsync<BlankAccountListItemView>(_blankAccountViewPrefab, root);
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