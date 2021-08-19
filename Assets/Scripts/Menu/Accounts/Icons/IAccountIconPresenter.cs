using CarSumo.DataModel.Accounts;
using UniRx;

namespace Menu.Accounts
{
    public interface IAccountIconPresenter
    {
        IReadOnlyReactiveProperty<Icon> Icon { get; }
    }
}