using UniRx;

namespace CarSumo.DataModel.Accounts
{
    public interface IAccountStorage
    {
        IReadOnlyReactiveCollection<Account> AllAccount { get; }
        IReadOnlyReactiveProperty<Account> ActiveAccount { get; }
    }
}