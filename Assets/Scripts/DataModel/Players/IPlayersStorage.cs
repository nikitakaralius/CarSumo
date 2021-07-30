using UniRx;

namespace CarSumo.DataModel.Players
{
    public interface IPlayersStorage
    {
        IReadOnlyReactiveCollection<UnityPlayer> AllPlayers { get; }
        IReadOnlyReactiveProperty<UnityPlayer> ActivePlayer { get; }
    }
}