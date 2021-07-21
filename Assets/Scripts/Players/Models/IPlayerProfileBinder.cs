using DataManagement.Players.Models;

namespace CarSumo.Players.Models
{
    public interface IPlayerProfileBinder
    {
        PlayerProfile BindFrom(Player player);
    }
}