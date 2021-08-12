using System;
using CarSumo.DataModel.Accounts;
using GuiBaseData.Accounts;
using UniRx;
using Zenject;

namespace Menu.Accounts
{
    public class ActiveAccountView : AccountView
    {
        private IAccountStorage _accountStorage;
        private IDisposable _subscription;

        [Inject]
        private void Construct(IAccountStorage accountStorage)
        {
            _accountStorage = accountStorage;
        }

        private void Awake()
        {
            _subscription = _accountStorage.ActiveAccount.Subscribe(ChangeAccount);
        }

        private void OnDestroy()
        {
            _subscription.Dispose();   
        }
    }
}