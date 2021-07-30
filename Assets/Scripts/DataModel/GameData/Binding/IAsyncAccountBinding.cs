using System.Threading.Tasks;
using CarSumo.DataModel.Players;

namespace CarSumo.Binding
{
    public interface IAsyncAccountBinding
    {
        Task<Account> BuildFromAsync(SerializableAccount account);
    }
}