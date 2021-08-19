using System;
using System.Collections.Generic;
using CarSumo.DataModel.Accounts;
using DataModel.GameData.Vehicles;
using Menu.Accounts.AccountEditor;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace Menu.Accounts
{
	public class AccountOperationsProvider : SerializedMonoBehaviour, INewAccountRecorder, IAccountEditor
	{
		private enum AccountOperationException
		{
			NotEnoughCharactersInName,
			AccountWithTheSameAlreadyExists
		}
		
		[Header("Required Components")]
		[SerializeField] private TMP_InputField _nameInputField;

		[Header("Preferences")]
		[SerializeField] private int _minNameSymbolsCount;
		[SerializeField] private IReadOnlyDictionary<AccountOperationException, string> _exceptionsDescriptions;
		
		private IAccountIconPresenter _iconPresenter;
		private IClientAccountStorageOperations _storageOperations;
		private IVehicleLayoutBuilder _layoutBuilder;
		private IServerAccountOperations _accountOperations;
		
		[Inject]
		private void Construct(IAccountIconPresenter iconPresenter,
			IClientAccountStorageOperations storageOperations,
			IVehicleLayoutBuilder layoutBuilder,
			IServerAccountOperations accountOperations)
		{
			_iconPresenter = iconPresenter;
			_storageOperations = storageOperations;
			_layoutBuilder = layoutBuilder;
			_accountOperations = accountOperations;
		}

		private void OnValidate()
		{
			_minNameSymbolsCount = Mathf.Clamp(_minNameSymbolsCount, 0, _nameInputField.characterLimit);
		}

		public AccountOperation RecordNewAccount() =>
			ValidateAccountOperation(BuildAccount(), validatedAccount =>
				_storageOperations.TryAddAccount(validatedAccount));

		public AccountOperation ChangeAccountValues(Account account) =>
			ValidateAccountOperation(account, validatedAccount =>
			{
				bool changeNameResult = _accountOperations.TryChangeName(validatedAccount, _nameInputField.text);

				if (changeNameResult)
					_accountOperations.ChangeIcon(validatedAccount, _iconPresenter.Icon.Value);

				return changeNameResult;
			});

		private AccountOperation ValidateAccountOperation(Account account, Func<Account, bool> checkedAddAction)
		{
			string exceptionDescription = null;
			
			if (_nameInputField.text.Length < _minNameSymbolsCount)
			{
				exceptionDescription = _exceptionsDescriptions[AccountOperationException.NotEnoughCharactersInName];
				return new AccountOperation(exceptionDescription, null);
			}

			if (checkedAddAction.Invoke(account))
				return new AccountOperation(exceptionDescription, account);

			exceptionDescription = _exceptionsDescriptions[AccountOperationException.AccountWithTheSameAlreadyExists];
			return new AccountOperation(exceptionDescription, null);
		}
		
		private Account BuildAccount() =>
			new Account(_nameInputField.text,
				_iconPresenter.Icon.Value,
				_layoutBuilder.Create(new DefaultVehicleLayout()));
	}
}