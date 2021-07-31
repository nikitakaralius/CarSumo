using CarSumo.DataModel.Accounts;

namespace CarSumo.DataModel.GameData.Accounts
{
    public interface IAccountSerialization
    {
        SerializableAccount SerializeFrom(Account account);
    }
}