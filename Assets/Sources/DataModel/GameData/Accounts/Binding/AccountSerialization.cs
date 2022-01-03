using CarSumo.DataModel.Accounts;

namespace CarSumo.DataModel.GameData.Accounts
{
    public class AccountSerialization : IAccountSerialization
    {
        public SerializableAccount SerializeFrom(Account account)
        {
            return new SerializableAccount()
            {
                Name = account.Name.Value,
                Icon = account.Icon.Value.Asset,
                VehicleLayout = account.VehicleDeck.ActiveVehicles
            };
        }
    }
}