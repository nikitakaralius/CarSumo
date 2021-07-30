using UniRx;

namespace CarSumo.DataModel.Players
{
    public interface IPlayersStorage
    {
        IReadOnlyReactiveCollection<Player> AllPlayers { get; }
        IReadOnlyReactiveProperty<Player> ActivePlayer { get; }
    }
}