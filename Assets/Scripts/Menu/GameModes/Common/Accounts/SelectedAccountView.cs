using System;
using System.Collections.Generic;
using System.Linq;
using AdvancedAudioSystem;
using CarSumo.DataModel.Accounts;
using CarSumo.Teams;
using GameModes;
using GuiBaseData.Accounts;
using Menu.Accounts;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.GameModes.Common.Accounts
{
	[RequireComponent(typeof(Button))]
	public class SelectedAccountView : AccountView, IAccountListRules, IAccountSelectHandler
	{
		[Header("Null Account Preferences")] 
		[SerializeField] private string _nullAccountName = "None";
		[SerializeField] private Sprite _nullAccountIcon;
		
		[Header("Selected Account View Preferences")]
		[SerializeField] private Team _team;
		[SerializeField] private AccountListView _accountList;

		private Account _selectedAccount;

		private IAccountStorage _accountStorage;
		private IAudioPlayer _audioPlayer;

		private IGameModePreferences _gameModePreferences;
		private IGameModeOperations _gameModeOperations;

		private IDisposable _changeAccountSubscription;

		[Inject]
		private void Construct(IAccountStorage storage, IAudioPlayer player, IGameModePreferences preferences,
			IGameModeOperations operations)
		{
			_accountStorage = storage;
			_audioPlayer = player;
			_gameModePreferences = preferences;
			_gameModeOperations = operations;
		}

		public bool SelectActivated => false;

		public IEnumerable<Account> AccountsToRender
			=> _accountStorage.AllAccounts.Where(account => account.Equals(_selectedAccount) == false);

		private void Awake()
		{
			_changeAccountSubscription = _gameModePreferences
									.GetAccountByTeam(_team)
									.Subscribe(OnAccountChanged);
		}

		private void Start()
		{
			Button button = GetComponent<Button>();
			
			Debug.Log(button);
			
			button.onClick.AddListener(() =>
			{
				_audioPlayer.Play();
				_accountList.Open(this, this);
			});
		}

		private void OnDestroy()
		{
			_changeAccountSubscription?.Dispose();
		}

		public void OnButtonSelected(AccountListItem element)
		{
			_gameModeOperations.RegisterAccount(_team, element.Account);
			_accountList.Close();
		}

		public void OnButtonDeselected(AccountListItem element)
		{
			
		}

		public void OnListItemCreated(AccountListItem item)
		{
			
		}

		private void OnAccountChanged(Account account)
		{
			if (account is null)
			{
				ChangeViewValues(_nullAccountName, _nullAccountIcon);
			}
			else
			{
				_selectedAccount = account;
				ChangeAccount(account);
			}
		}
	}
}