using System;
using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;
using DataModel.GameData.Vehicles;
using DataModel.Vehicles;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CarSumo.DataModel.GameData.Accounts
{
    public class AddressableAccountBinding : IAsyncAccountBinding
    {
        private readonly IVehicleLayoutBuilder _layoutBuilder;
        private readonly string _defaultIconPath;

        public AddressableAccountBinding(string defaultIconPath, IVehicleLayoutBuilder layoutBuilder)
        {
            if (defaultIconPath is null)
            {
                throw new ArgumentException($"{nameof(defaultIconPath)} must be defined");
            }
            
            _defaultIconPath = defaultIconPath;
            _layoutBuilder = layoutBuilder;
        }

        public async Task<Account> ToAccountAsync(SerializableAccount account)
        {
            Sprite sprite = await LoadAccountIcon(account.Icon);
            Icon icon = new Icon(sprite, account.Icon);
            IVehicleLayout vehicleLayout = _layoutBuilder.Create(account.VehicleLayout);
            return new Account(account.Name, icon, vehicleLayout);
        }

        private async Task<Sprite> LoadAccountIcon(string key)
        {
            return await Addressables.LoadAssetAsync<Sprite>(key ?? _defaultIconPath).Task;
        }
    }
}