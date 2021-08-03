using CarSumo.DataModel.Accounts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

namespace Menu.Accounts
{
    public class SelectedAccountView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _name;

        private IAccountStorage _accountStorage;

        [Inject]
        private void Construct(IAccountStorage accountStorage)
        {
            _accountStorage = accountStorage;
        }

        private void Awake()
        {
            _accountStorage.ActiveAccount.Subscribe(ChangeActiveAccount);
        }

        private void ChangeActiveAccount(Account account)
        {
            _name.text = account.Name.Value;
            _icon.sprite = account.Icon.Value.Sprite;
        }
    }
}