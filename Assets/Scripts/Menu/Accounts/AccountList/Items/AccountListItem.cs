using AdvancedAudioSystem;
using CarSumo.DataModel.Accounts;
using Menu.Buttons;
using UnityEngine;
using Zenject;

namespace Menu.Accounts
{
	[RequireComponent(typeof(AccountListItemDragHandler))]
	public class AccountListItem : SelectableButton<AccountListItem>
	{
		[SerializeField] private AccountListItemView _view;

		private IAudioPlayer _audioPlayer;

		[Inject]
		private void Construct(IAudioPlayer audioPlayer)
		{
			_audioPlayer = audioPlayer;
		}
		
		public Account Account { get; private set; }

		public void Initialize(Account account, IButtonSelectHandler<AccountListItem> selectHandler, IReadOnlyDragHandlerData dragData)
		{
			Initialize(this, selectHandler);

			Account = account;

			_view.ChangeAccount(Account);
			
			AccountListItemDragHandler dragHandler = GetComponent<AccountListItemDragHandler>();
			dragHandler.Initialize(Account, Button, dragData);
		}

		protected override void OnButtonSelectedInternal()
		{
			_audioPlayer.Play();
			_view.ConfigureBackground(true);
		}

		protected override void OnButtonDeselectedInternal()
		{
			_view.ConfigureBackground(false);
		}
	}
}