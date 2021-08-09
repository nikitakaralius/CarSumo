using System;
using AdvancedAudioSystem;
using CarSumo.DataModel.Accounts;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Accounts
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(AccountListItemDragHandler))]
    public class AccountListItem : AccountView
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Sprite _activatedBackground;
        [SerializeField] private Sprite _normalBackground;

        private IAccountStorage _accountStorage;
        private IClientAccountOperations _accountOperations;
        private IAudioPlayer _audioPlayer;
        private IDisposable _subscription;

        [Inject]
        private void Construct(IAccountStorage accountStorage, IClientAccountOperations accountOperations, IAudioPlayer audioPlayer)
        {
            _accountStorage = accountStorage;
            _accountOperations = accountOperations;
            _audioPlayer = audioPlayer;
        }

        public void Initialize(Account account, Transform originalParent, Transform draggingParent, LayoutGroup layoutGroup)
        {
            ChangeAccount(account);
            gameObject.name = account.Name.Value;

            Button button = GetComponent<Button>();
            button.onClick.AddListener(() => SetActiveAccount(account));
            button.onClick.AddListener(_audioPlayer.Play);

            AccountListItemDragHandler dragHandler = GetComponent<AccountListItemDragHandler>();
            dragHandler.Initialize(account, button, originalParent, draggingParent, layoutGroup);
            
            _subscription = _accountStorage
                            .ActiveAccount
                            .Subscribe(activeAccount => UpdateAccount(account, activeAccount));
        }

        private void OnDestroy()
        {
            _subscription.Dispose();
        }

        private void UpdateAccount(Account currentAccount, Account activeAccount)
        {
            ConfigureBackground(currentAccount.Equals(activeAccount));
        }

        private void SetActiveAccount(Account account)
        {
            _accountOperations.SetActive(account);
            ConfigureBackground(accountActive: true);
        }

        private void ConfigureBackground(bool accountActive)
        {
            _backgroundImage.sprite = accountActive ? _activatedBackground : _normalBackground;
        }
    }
}