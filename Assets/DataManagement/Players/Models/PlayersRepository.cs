using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace DataManagement.Players.Models
{
    [System.Serializable]
    [JsonObject(MemberSerialization.Fields)]
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

        public IEnumerable<Player> Players => Items;
    }
}