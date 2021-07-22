using System.Collections.Generic;
using CarSumo.Players.Models;

namespace CarSumo.Menu.Models
{
    public interface IPlayerProfilesProvider
    {
        PlayerProfile SelectedPlayer { get; }
        IEnumerable<PlayerProfile> OtherPlayers { get; }
    }
}