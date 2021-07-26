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
        public int SelectedPlayerIndex;

        public Player SelectedPlayer => Items[SelectedPlayerIndex];

        public bool TryMakePlayerSelected(Player player)
        {
            int index = Items.IndexOf(player);

            if (index == -1)
                return false;

            SelectedPlayerIndex = index;
            return true;
        }
        
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