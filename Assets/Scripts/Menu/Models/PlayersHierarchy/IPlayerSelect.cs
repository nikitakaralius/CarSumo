using CarSumo.Players.Models;

namespace CarSumo.Menu.Models
{
    public interface IPlayerSelect
    {
        void MakePlayerSelected(PlayerProfile newSelected);
    }
}