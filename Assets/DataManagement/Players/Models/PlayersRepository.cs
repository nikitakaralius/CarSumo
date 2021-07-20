using System.Collections.Generic;
using System.Linq;

namespace DataManagement.Players.Models
{
    [System.Serializable]
    public class PlayersRepository : IPlayersRepository
    {
        public List<Player> Items;

        public bool TryAddPlayer(Player player)
        {
            if (Items.Any(otherPlayer => otherPlayer.Name == player.Name))
                return false;
            
            Items.Add(player);
            return true;
        }

        public IReadOnlyList<Player> Players => Items;
    }
}