using System.Threading.Tasks;
using CarSumo.DataModel.Players;

namespace CarSumo.Binding
{
    public interface IAsyncPlayerBinding
    {
        Task<Player> BuildFromAsync(SerializablePlayer player);
    }
}