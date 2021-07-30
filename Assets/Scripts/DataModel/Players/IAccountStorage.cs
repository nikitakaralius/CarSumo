using UniRx;

namespace CarSumo.DataModel.Players
{
    public interface IAccountStorage
    {
        IReadOnlyReactiveCollection<Account> AllPlayers { get; }
        IReadOnlyReactiveProperty<Account> ActivePlayer { get; }
    }
}