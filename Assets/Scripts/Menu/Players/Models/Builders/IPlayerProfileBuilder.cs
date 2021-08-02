using System.Threading.Tasks;
using DataManagement.Players.Models;

namespace CarSumo.Players.Models
{
    public interface IPlayerProfileBuilder
    {
        Task<PlayerProfile> BuildFrom(Player player);
    }
}