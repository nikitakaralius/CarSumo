using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;
using CarSumo.DataModel.GameResources;
using CarSumo.Teams;
using GameModes;
using JetBrains.Annotations;
using Menu.Buttons;
using Services.Instantiate;
using Sirenix.Utilities;
using TweenAnimations;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Menu.Accounts
{
	public class AccountListView : MonoBehaviour, IAccountSelectHandler, IAccountListRules
    {
	    [Header("Account Views")]
	    [SerializeField] private AssetReferenceGameObject _accountViewPrefab;
	    [SerializeField] private AssetReferenceGameObject _blankAccountViewPrefab;
	    [SerializeField] private Team _onItemSelectedTeamRegister;

	    [Header("Item Drag Handler Preferences")]
	    [SerializeField] private DragHandlerData _dragHandlerData;
	    [SerializeField] private SizeTweenAnimation _dragAnimation;
	    [Range(0.0f, 1.0f)] 
	    [SerializeField] private float _requiredHoldTime;

	    private IAccountListRules _rules;
	    private IButtonSelectHandler<AccountListItem> _itemSelectHandler;

	    private IAsyncInstantiation _instantiation;
	    private IAccountStorage _accountStorage;
	    private IResourceStorage _resourceStorage;
	    private IGameModeOperations _gameModeOperations;

	    private IClientAccountOperations _accountOperations;
	    private IClientAccountStorageOperations _accountStorageOperations;

	    private IDisposable _accountsChangedSubscription;

	    private readonly List<AccountListItem> _items = new List<AccountListItem>();
	    private readonly List<GameObject> _allViews = new List<GameObject>();

	    private AccountListItem _activeAccountListItem;
	    
	    [Inject]
	    private void Construct(IAsyncInstantiation instantiation,
		    					IAccountStorage accountStorage,
		    					IResourceStorage resourceStorage,
		    					IClientAccountOperations accountOperations,
		                        IGameModeOperations gameModeOperations,
		                        IClientAccountStorageOperations accountStorageOperations)
	    {
		    _instantiation = instantiation;
		    _accountStorage = accountStorage;
		    _resourceStorage = resourceStorage;
		    _accountOperations = accountOperations;
		    _gameModeOperations = gameModeOperations;
		    _accountStorageOperations = accountStorageOperations;
	    }
	    
	    public bool SelectActivated => true;

	    public IEnumerable<Account> AccountsToRender => _accountStorage.AllAccounts;
	    
	    private Transform ItemsRoot => _dragHandlerData.ContentParent;

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
		    Open(this, this);
	    }

	    public void Open(IAccountListRules rules, IButtonSelectHandler<AccountListItem> selectHandler)
	    {
		    _rules = rules;
		    _itemSelectHandler = selectHandler;
		    gameObject.SetActive(true);
		    
		    FillList();
	    }

	    public void Close()
	    {
		    gameObject.SetActive(false);
	    }
	    
	    public void OnListItemCreated(AccountListItem item)
	    {
			item.gameObject
				.AddComponent<AccountListItemDragHandler>()
				.Initialize(_requiredHoldTime, _accountStorageOperations, item.Account, item.Button, _dragAnimation, _dragHandlerData);
	    }

	    public void OnButtonSelected(AccountListItem element)
	    {
		    _activeAccountListItem = element;
		    _accountOperations.SetActive(_activeAccountListItem.Account);
		    _gameModeOperations.RegisterAccount(_onItemSelectedTeamRegister, element.Account);
		    _items.Where(item => item != _activeAccountListItem).ForEach(item => item.SetSelected(false));
	    }

	    public void OnButtonDeselected(AccountListItem element)
	    {
		    if (element == _activeAccountListItem)
		    {
			    element.SetSelected(true);
		    }
	    }

	    private async void FillList()
	    {
		    ClearPrevious();

		    IEnumerable<AccountListItem> accountListItems = await CreateAccountListItems(_rules.AccountsToRender, ItemsRoot);
		    IEnumerable<GameObject> blankAccountViews = await CreateBlankAccountViews(ItemsRoot);

		    IEnumerable<GameObject> accountListViews = accountListItems.Select(item => item.gameObject);

		    _activeAccountListItem = GetActiveAccountListItem(_accountStorage.ActiveAccount.Value, accountListItems);
		    _activeAccountListItem?.SetSelected(_rules.SelectActivated);
		    
		    _allViews.AddRange(accountListViews);
		    _allViews.AddRange(blankAccountViews);
		    
		    _items.AddRange(accountListItems);
	    }
	    
	    private void ClearPrevious()
	    {
		    _activeAccountListItem = null;
		    
		    foreach (GameObject view in _allViews)
		    {
			    Destroy(view);
		    }
		    
		    _allViews.Clear();
		    _items.Clear();
	    }

	    [CanBeNull]
	    private AccountListItem GetActiveAccountListItem(Account activeAccount, IEnumerable<AccountListItem> allAccounts)
	    {
		    return allAccounts.FirstOrDefault(accountListItem => accountListItem.Account.Equals(activeAccount));
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
			    _rules.OnListItemCreated(listItem);
			    views.Add(listItem);
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