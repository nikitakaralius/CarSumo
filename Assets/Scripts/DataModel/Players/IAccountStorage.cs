using UniRx;

namespace CarSumo.DataModel.Accounts
{
    public interface IAccountStorage
    {
        IReadOnlyReactiveCollection<Account> AllPlayers { get; }
        IReadOnlyReactiveProperty<Account> ActivePlayer { get; }
    }
}