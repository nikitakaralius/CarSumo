using AdvancedAudioSystem;
using CarSumo.DataModel.Accounts;
using Menu.Accounts.AccountEditor;
using Menu.Buttons;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Accounts
{
	public class AccountListItem : SelectableButton<AccountListItem>
	{
		[SerializeField] private AccountListItemView _view;
		[SerializeField] private Button _editButton;
		
		private IAudioPlayer _audioPlayer;
		private IAccountEditorPopup _editorPopup;

		[Inject]
		private void Construct(IAudioPlayer audioPlayer, IAccountEditorPopup editorPopup)
		{
			_audioPlayer = audioPlayer;
			_editorPopup = editorPopup;
		}
		
		public Account Account { get; private set; }

		public void Initialize(Account account, IButtonSelectHandler<AccountListItem> selectHandler)
		{
			Initialize(this, selectHandler);
			Account = account;
			_view.ChangeAccount(Account);
			
			_editButton.onClick.AddListener(() => _editorPopup.Open(Account));
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