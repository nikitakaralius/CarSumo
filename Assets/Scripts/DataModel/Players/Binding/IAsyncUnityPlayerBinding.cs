using System.Threading.Tasks;

namespace CarSumo.DataModel.Players.Binding
{
    public interface IAsyncUnityPlayerBinding
    {
        Task<UnityPlayer> BindFromAsync(SerializablePlayer player);
    }
}