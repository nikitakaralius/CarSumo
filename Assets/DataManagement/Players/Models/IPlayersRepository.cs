using System.Collections.Generic;

namespace DataManagement.Players.Models
{
    public interface IPlayersRepository
    {
        IReadOnlyList<Player> Players { get; }
        bool TryAddPlayer(Player player);
    }
}