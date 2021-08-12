using System;
using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using CarSumo.Teams;
using DataModel.GameData.Accounts.Extensions;
using Menu.Accounts;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.GameModes.OneDevice.Accounts
{
	public class AccountsPreferences : SerializedMonoBehaviour, IAccountsPreferences
	{
		[SerializeField] 
		private IReadOnlyDictionary<Team, AccountView> _accountViews = new Dictionary<Team, AccountView>();
		
		private IAccountStorage _accountStorage;
		private IDisposable _activeAccountSubscription;

		private readonly Dictionary<Team, Account> _accountsToRegister = new Dictionary<Team, Account>
		{
			{Team.Blue, null},
			{Team.Red, null}
		};

		[Inject]
		private void Construct(IAccountStorage accountStorage)
		{
			_accountStorage = accountStorage;
		}

		public IReadOnlyDictionary<Team, Account> AccountsToRegister => _accountsToRegister;

		private void OnEnable()
		{
			_activeAccountSubscription = _accountStorage.ActiveAccount
				.Subscribe(OnActiveAccountChanged);
		}

		private void OnDisable()
		{
			_activeAccountSubscription?.Dispose();
		}

		private void OnActiveAccountChanged(Account activeAccount)
		{
			RegisterAndRenderAccount(Team.Blue, activeAccount);
			UpdateAccounts();
		}

		private void UpdateAccounts()
		{
			Account blueTeamAccount = _accountsToRegister[Team.Blue] ?? _accountStorage.ActiveAccount.Value;
			Account redTeamAccount = _accountsToRegister[Team.Red] ?? _accountStorage.GetAccountsExceptActive().First();

			RegisterAndRenderAccount(Team.Blue, blueTeamAccount);
			RegisterAndRenderAccount(Team.Red, redTeamAccount);
		}
		
		private void RegisterAndRenderAccount(Team team, Account account)
		{
			_accountsToRegister[team] = account;
			_accountViews[team].ChangeAccount(account);
		}
	}
}