using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;
using DataModel.GameData.Vehicles;
using DataModel.Vehicles;
using Services.Instantiate;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Menu.Vehicles.Layout
{
    public abstract class VehicleLayoutView<T> : SerializedMonoBehaviour where T : Component
    {
        [SerializeField] private IVehicleAssetsProvider _assetsProvider;

        private IAccountStorage _accountStorage;
        private IAsyncInstantiation _instantiation;

        private List<T> _items = new List<T>();

        private IDisposable _accountChangedSubscription;
        private IDisposable _layoutChangedSubscription;
        
        [Inject]
        private void Construct(IAccountStorage accountStorage, IAsyncInstantiation instantiation)
        {
            _accountStorage = accountStorage;
            _instantiation = instantiation;
        }

        public IReadOnlyList<T> Items => _items;
        private IReadOnlyReactiveProperty<Account> ActiveAccount => _accountStorage.ActiveAccount;
        private IReadOnlyReactiveProperty<IVehicleLayout> Layout => _accountStorage.ActiveAccount.Value.VehicleLayout;

        private void OnEnable()
        {
            _accountChangedSubscription = ActiveAccount.Subscribe(OnActiveAccountChanged);
        }

        private void OnDisable()
        {
            _layoutChangedSubscription?.Dispose();
            _accountChangedSubscription?.Dispose();
        }

        public abstract Task SpawnLayoutAsync(IVehicleLayout layout);

        public void Clear()
        {
            foreach (T vehicle in _items)
            {
                Destroy(vehicle.gameObject);
            }
        }

        private void OnActiveAccountChanged(Account account)
        {
            _layoutChangedSubscription?.Dispose();
            _layoutChangedSubscription = Layout.Subscribe(async layout => await SpawnLayoutAsync(layout));
        }

        private async Task<IEnumerable<T>> GetVehicleItemsAsync(IEnumerable<VehicleId> vehicleIds, Transform parent)
        {
            var vehicles = new List<T>();
            foreach (VehicleId vehicleId in vehicleIds)
            {
                AssetReferenceGameObject asset = _assetsProvider.GetAssetByVehicleId(vehicleId);
                T vehicleItem = await _instantiation.InstantiateAsync<T>(asset, parent);
                vehicles.Add(vehicleItem);
            }
            
            return vehicles;
        }
    }
}