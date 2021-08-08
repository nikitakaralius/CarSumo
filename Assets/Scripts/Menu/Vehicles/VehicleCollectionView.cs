using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel.GameData.Vehicles;
using DataModel.Vehicles;
using Services.Instantiate;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Menu.Vehicles
{
    public abstract class VehicleCollectionView<T> : SerializedMonoBehaviour where T : Component
    {
        [SerializeField] private IVehicleAssetsProvider _assetsProvider;

        private IAsyncInstantiation _instantiation;
        private List<T> _items = new List<T>();

        [Inject]
        private void Construct(IAsyncInstantiation instantiation)
        {
            _instantiation = instantiation;
        }

        protected abstract Transform CollectionRoot { get; }
        
        protected IReadOnlyList<T> Items => _items;
        
        protected abstract void ProcessCreatedLayout(IEnumerable<T> layout);

        protected async Task SpawnCollectionAsync(IEnumerable<VehicleId> vehicles)
        {
            Clear();
            
            IEnumerable<T> items = await GetVehicleItemsAsync(vehicles, CollectionRoot);
            _items = items.ToList();
            ProcessCreatedLayout(_items);
        }
        
        private void Clear()
        {
            foreach (T vehicle in _items)
            {
                Destroy(vehicle.gameObject);
            }
            
            _items.Clear();
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