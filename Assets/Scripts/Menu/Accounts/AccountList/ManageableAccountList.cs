using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using CarSumo.Teams;
using GameModes;
using JetBrains.Annotations;
using Sirenix.Utilities;
using TweenAnimations;
using UnityEngine;
using Zenject;

namespace Menu.Accounts
{
	public class ManageableAccountList : MonoBehaviour, IAccountListRules, IAccountSelectHandler, IAccountListObserver
	{
		[Header("Select Preferences")] 
		[SerializeField] private Team _selectedItemTeamToRegister;

		[Header("Item Dragging Preferences")]
		[Range(0.0f, 0.5f)] 
		[SerializeField] private float _requiredHoldTime;
		[SerializeField] private DragHandlerData _dragHandlerData;
		[SerializeField] private SizeTweenAnimation _dragAnimation;

		private IAccountStorage _accountStorage;
		private IGameModeOperations _gameModeOperations;
		private IClientAccountOperations _accountOperations;
		private IClientAccountStorageOperations _storageOperations;

		private AccountListItem _activeAccountListItem;

		private readonly List<AccountListItem> _items = new List<AccountListItem>();

		[Inject]
		private void Construct(IAccountStorage accountStorage,
								IGameModeOperations gameModeOperations,
								IClientAccountStorageOperations storageOperations,
								IClientAccountOperations accountOperations)
		{
			_accountStorage = accountStorage;
			_gameModeOperations = gameModeOperations;
			_storageOperations = storageOperations;
			_accountOperations = accountOperations;
		}

		public IEnumerable<Account> AccountsToRender => _accountStorage.AllAccounts;

		public void OnButtonSelected(AccountListItem element)
		{
			_activeAccountListItem = element;
			
			_accountOperations.SetActive(element.Account);
			_gameModeOperations.RegisterAccount(_selectedItemTeamToRegister, element.Account);

			NotifyOtherAccounts(_items, element);
		}

		public void OnButtonDeselected(AccountListItem element)
		{
			if (element == _activeAccountListItem)
			{
				element.SetSelected(true);
			}
		}

		public void OnAccountCreated(AccountListItem listItem)
		{
			AddDragHandlerComponent(listItem);
		}

		public void OnAllItemsCreated(IEnumerable<AccountListItem> items)
		{
			_items.Clear();
			_items.AddRange(items);

			_activeAccountListItem = GetActiveAccountListItem(_accountStorage.ActiveAccount.Value, _items);
			_activeAccountListItem?.SetSelected(true);
		}

		private void NotifyOtherAccounts(IEnumerable<AccountListItem> allAccounts, AccountListItem activeAccount)
		{
			allAccounts
				.Where(item => item != activeAccount)
				.ForEach(item => item.SetSelected(false));
		}

		private void AddDragHandlerComponent(AccountListItem listItem)
		{
			listItem.gameObject
				.AddComponent<AccountListItemDragHandler>()
				.Initialize(_requiredHoldTime,
							_storageOperations,
							listItem.Account,
							listItem.Button,
							_dragAnimation,
							_dragHandlerData);
		}

		[CanBeNull]
		private AccountListItem GetActiveAccountListItem(Account activeAccount, IEnumerable<AccountListItem> allAccounts)
		{
			return allAccounts.FirstOrDefault(accountListItem => accountListItem.Account.Equals(activeAccount));
		}
	}
}