using System.Collections.Generic;
using System.Linq;

namespace DataManagement.Players.Models
{
    [System.Serializable]
    public class PlayersRepository
    {
        public List<Player> Players;

        public bool TryAddPlayer(Player player)
        {
            if (Players.Any(otherPlayer => otherPlayer.Name == player.Name))
                return false;
            
            Players.Add(player);
            return true;
        }
    }
}