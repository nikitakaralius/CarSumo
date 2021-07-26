using System.Collections.Generic;

namespace DataManagement.Players.Models
{
    public interface IPlayersRepository
    {
        IReadOnlyList<Player> Players { get; }
        Player SelectedPlayer { get; }
        bool TryAddPlayer(Player player);
        bool TryMakePlayerSelected(Player player);
    }
}