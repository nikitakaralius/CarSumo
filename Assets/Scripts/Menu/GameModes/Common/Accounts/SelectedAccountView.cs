using AdvancedAudioSystem;
using CarSumo.DataModel.Accounts;
using CarSumo.Teams;
using Menu.Accounts;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.GameModes.Common.Accounts
{
	[RequireComponent(typeof(Button))]
	public class SelectedAccountView : AccountView
	{
		[SerializeField] private Team _team;
		[SerializeField] private AccountListView _accountList;

		private Account _selectedAccount;

		private IAccountStorage _accountStorage;
		private IAudioPlayer _audioPlayer;
		
		protected override void OnAccountChanged(Account account)
		{
			_selectedAccount = account;
		}
	}
}