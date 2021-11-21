using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel.GameData.Vehicles;
using DataModel.Vehicles;
using Menu.Extensions;
using Services.Instantiate;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Menu.Vehicles
{
	public abstract class VehicleCollectionView<T> : SerializedMonoBehaviour where T : Component
	{
		[SerializeField] private IVehicleAssets _assets;

		private IAsyncInstantiation _instantiation;
		private readonly List<T> _items = new List<T>();

		[Inject]
		private void Construct(IAsyncInstantiation instantiation)
		{
			_instantiation = instantiation;
		}

		protected abstract Transform CollectionRoot { get; }

		protected IReadOnlyList<T> Items => _items;

		protected virtual void OnDisable()
		{
			_items.DestroyAndClear();
		}
		
		protected abstract void ProcessCreatedCollection(IEnumerable<T> layout);

		protected async Task SpawnCollectionAsync(IEnumerable<VehicleId> vehicles)
		{
			_items.DestroyAndClear();

			IEnumerable<T> items = await GetVehicleItemsAsync(vehicles, CollectionRoot);
			_items.AddRange(items);
			ProcessCreatedCollection(_items);
		}

		private async Task<IEnumerable<T>> GetVehicleItemsAsync(IEnumerable<VehicleId> vehicleIds, Transform parent)
		{
			var vehicles = new List<T>();
			foreach (VehicleId vehicleId in vehicleIds)
			{
				AssetReferenceGameObject asset = _assets.GetAssetByVehicleId(vehicleId);
				T vehicleItem = await _instantiation.InstantiateAsync<T>(asset, parent);
				vehicles.Add(vehicleItem);
			}

			return vehicles;
		}
	}
}