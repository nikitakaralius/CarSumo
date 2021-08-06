using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;
using DataModel.GameData.Vehicles;
using DataModel.Vehicles;
using Services.Instantiate;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Menu.Vehicles
{
    internal class VehicleLayoutPreview : MonoBehaviour
    {
        private IVehicleAssetsProvider _assetsProvider;
        private IAccountStorage _accountStorage;
        private IAsyncInstantiation _instantiation;

        private IDisposable _accountChangedSubscription;
        private IDisposable _layoutChangedSubscription;

        private GameObject[] _vehicleLayout = new GameObject[0];
        private int _selectedVehicleIndex;

        [Inject]
        private void Construct(IVehicleAssetsProvider assetsProvider, IAccountStorage accountStorage, IAsyncInstantiation instantiation)
        {
            _assetsProvider = assetsProvider;
            _accountStorage = accountStorage;
            _instantiation = instantiation;
        }

        private IReadOnlyReactiveProperty<Account> ActiveAccount => _accountStorage.ActiveAccount;

        private void Start()
        {
            _accountChangedSubscription = ActiveAccount.Subscribe(OnActiveAccountChanged);
        }

        private void OnDestroy()
        {
            _layoutChangedSubscription?.Dispose();
            _accountChangedSubscription?.Dispose();
        }

        private void OnActiveAccountChanged(Account account)
        {
            _layoutChangedSubscription?.Dispose();
            _layoutChangedSubscription = account.VehicleLayout.Subscribe(async layout => await SpawnLayout(layout));
        }

        private async Task SpawnLayout(IVehicleLayout layout)
        {
            ClearLayout();
            
            _vehicleLayout = await GetVehicleAssets(layout.ActiveVehicles);
            foreach (GameObject vehicle in _vehicleLayout)
            {
                vehicle.SetActive(false);
            }
            _selectedVehicleIndex = 0;
            _vehicleLayout[_selectedVehicleIndex].SetActive(true);
        }

        private void ClearLayout()
        {
            foreach (GameObject vehicle in _vehicleLayout)
            {
                Destroy(vehicle);
            }
        }

        private async Task<GameObject[]> GetVehicleAssets(IEnumerable<VehicleId> vehicleIds)
        {
            var vehicles = new List<GameObject>();
            foreach (VehicleId vehicleId in vehicleIds)
            {
                AssetReferenceGameObject asset = _assetsProvider.GetAssetByVehicleId(vehicleId);
                MenuVehicle vehicle = await _instantiation.InstantiateAsync<MenuVehicle>(asset);
                vehicles.Add(vehicle.gameObject);
            }
            return vehicles.ToArray();
        }
    }
}