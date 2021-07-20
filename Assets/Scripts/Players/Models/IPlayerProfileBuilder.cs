using DataManagement.Players.Models;

namespace CarSumo.Players.Models
{
    public interface IPlayerProfileBuilder
    {
        PlayerProfile BuildFrom(Player player);
    }
}