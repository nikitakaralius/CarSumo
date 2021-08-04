using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;

namespace CarSumo.DataModel.GameData.Accounts
{
    public interface IAsyncAccountBinding
    {
        Task<Account> ToAccountAsync(SerializableAccount account);
    }
}