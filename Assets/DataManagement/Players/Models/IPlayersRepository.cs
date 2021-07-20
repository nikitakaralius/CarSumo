using System.Collections.Generic;

namespace DataManagement.Players.Models
{
    public interface IPlayersRepository
    {
        IEnumerable<Player> Players { get; }
        bool TryAddPlayer(Player player);
    }
}