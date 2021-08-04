using CarSumo.DataModel.Accounts;
using UniRx;

namespace Menu.Accounts
{
    public class NewAccountIcon : IAccountIconPresenter, IAccountIconReceiver
    {
        public IReadOnlyReactiveProperty<Icon> Icon => _icon;

        private ReactiveProperty<Icon> _icon;

        public void ReceiveIcon(Icon icon)
        {
            _icon.Value = icon;
        }
    }
}