using UniRx;

namespace CarSumo.DataModel.Accounts
{
    public interface IAccountStorage
    {
        IReadOnlyReactiveCollection<Account> AllAccounts { get; }
        IReadOnlyReactiveProperty<Account> ActiveAccount { get; }
    }
}