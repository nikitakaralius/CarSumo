using System;
using System.Collections.Generic;
using System.Linq;
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
    public class VehicleLayoutView : SerializedMonoBehaviour
    {
        [SerializeField] private IVehicleAssetsProvider _cardAssets;

        private IAccountStorage _accountStorage;
        private IAsyncInstantiation _instantiation;

        private CompositeDisposable _disposables = new CompositeDisposable();
        
        [Inject]
        private void Construct(IAccountStorage accountStorage, IAsyncInstantiation instantiation)
        {
            _accountStorage = accountStorage;
            _instantiation = instantiation;
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private async Task CreateLayout(IVehicleLayout layout)
        {
            IEnumerable<AssetReferenceGameObject> layoutAssets = GetLayoutAssets(layout.ActiveVehicles);
            foreach (AssetReferenceGameObject asset in layoutAssets)
            {
                
            }
        }

        private IEnumerable<AssetReferenceGameObject> GetLayoutAssets(IEnumerable<VehicleId> vehicles)
        {
            return vehicles.Select(id => _cardAssets.GetAssetByVehicleId(id));
        }
    }
}