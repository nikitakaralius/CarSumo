using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;

namespace CarSumo.Binding
{
    public interface IAsyncAccountBinding
    {
        Task<Account> BindFromAsync(SerializableAccount account);
    }
}