using System;
using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;
using DataModel.GameData.Vehicles;
using DataModel.Vehicles;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CarSumo.Binding
{
    public class AddressableAccountBinding : IAsyncAccountBinding
    {
        private readonly IVehicleLayoutFactory _layoutFactory;
        private readonly string _defaultIconPath;

        public AddressableAccountBinding(string defaultIconPath, IVehicleLayoutFactory layoutFactory)
        {
            if (defaultIconPath is null)
            {
                throw new ArgumentException($"{nameof(defaultIconPath)} must be defined");
            }
            
            _defaultIconPath = defaultIconPath;
            _layoutFactory = layoutFactory;
        }

        public async Task<Account> BindFromAsync(SerializableAccount account)
        {
            Sprite icon = await LoadAccountIcon(account.Icon);
            IVehicleLayout vehicleLayout = _layoutFactory.Create(account.VehicleLayout);
            return new Account(account.Name, icon, vehicleLayout);
        }

        private async Task<Sprite> LoadAccountIcon(string key)
        {
            return await Addressables.LoadAssetAsync<Sprite>(key ?? _defaultIconPath).Task;
        }
    }
}