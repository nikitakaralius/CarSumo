using CarSumo.DataModel.Accounts;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Accounts
{
    [RequireComponent(typeof(Button))]
    public class AccountListItem : AccountView
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Sprite _activatedBackground;
        [SerializeField] private Sprite _normalBackground;

        private IAccountStorage _accountStorage;
        private IClientAccountOperations _accountOperations;

        [Inject]
        private void Construct(IAccountStorage accountStorage, IClientAccountOperations accountOperations)
        {
            _accountStorage = accountStorage;
            _accountOperations = accountOperations;
        }

        public void Initialize(Account account)
        {
            ChangeAccount(account);
            GetComponent<Button>().onClick.AddListener(() => SetActiveAccount(account));

            _accountStorage.ActiveAccount.Subscribe(activeAccount => UpdateAccount(account, activeAccount));
        }

        private void UpdateAccount(Account currentAccount, Account activeAccount)
        {
            if (currentAccount.Equals(activeAccount))
                return;

            _backgroundImage.sprite = _normalBackground;
        }

        private void SetActiveAccount(Account account)
        {
            _accountOperations.SetActive(account);
            _backgroundImage.sprite = _activatedBackground;
        }
    }
}