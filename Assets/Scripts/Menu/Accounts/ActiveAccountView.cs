using CarSumo.DataModel.Accounts;
using UniRx;
using Zenject;

namespace Menu.Accounts
{
    public class ActiveAccountView : AccountView
    {
        private IAccountStorage _accountStorage;

        [Inject]
        private void Construct(IAccountStorage accountStorage)
        {
            _accountStorage = accountStorage;
        }

        private void Awake()
        {
            _accountStorage.ActiveAccount.Subscribe(ChangeAccount);
        }
    }
}