using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;
using DataModel.GameData.Vehicles;
using DataModel.Vehicles;
using Menu.Extensions;
using Services.Instantiate;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Sources.Menu.Vehicles.Cards
{
	public class BoughtCardsView : SerializedMonoBehaviour
	{
		[SerializeField, Required] private IVehicleAssets _assets;
		[SerializeField, Required, SceneObjectsOnly] private Transform _root;
		
		[Inject] private readonly IAsyncInstantiation _instantiation;
		[Inject] private readonly IAccountStorage _accountStorage;
		[Inject] private readonly IVehicleStorage _vehicleStorage;

		private readonly List<GameObject> _instances = new List<GameObject>();

		private readonly CompositeDisposable _accountStorageSubscriptions = new CompositeDisposable();
		private readonly CompositeDisposable _accountSubscriptions = new CompositeDisposable();
		private readonly CompositeDisposable _vehicleStorageSubscriptions = new CompositeDisposable();

		private void OnEnable()
		{
			_accountStorage
				.ActiveAccount
				.Subscribe(account =>
				{
					RebuildAvailableVehicles(account);
					
					_accountSubscriptions.Dispose();
					_accountSubscriptions.Clear();

					account
						.VehicleDeck
						.ObserveLayoutCompletedChanging()
						.Subscribe(_ => RebuildAvailableVehicles(account))
						.AddTo(_accountSubscriptions);
				})
				.AddTo(_accountStorageSubscriptions);

			_vehicleStorage
				.BoughtVehicles
				.ObserveCountChanged()
				.Subscribe(_ => RebuildAvailableVehicles(_accountStorage.ActiveAccount.Value))
				.AddTo(_vehicleStorageSubscriptions);
		}

		private void OnDisable()
		{
			_accountSubscriptions.Dispose();
			_accountStorageSubscriptions.Dispose();
			_vehicleStorageSubscriptions.Dispose();
		}

		private async void RebuildAvailableVehicles(Account activeAccount)
		{
			IEnumerable<VehicleId> vehiclesToRender = _vehicleStorage
				.BoughtVehicles
				.RemoveFirstOccurrences(activeAccount.VehicleDeck.ActiveVehicles);

			_instances.DestroyAndClear();

			IEnumerable<VehicleCardView> createdCards = await CreateVehicleCards(vehiclesToRender);
			_instances.AddRange(createdCards.Select(card => card.gameObject));
		}

		private async Task<IEnumerable<VehicleCardView>> CreateVehicleCards(IEnumerable<VehicleId> vehicles)
		{
			var cards = new List<VehicleCardView>();
			
			foreach (VehicleId vehicleId in vehicles)
			{
				AssetReferenceGameObject asset = _assets.GetAssetByVehicleId(vehicleId);
				var card = await _instantiation.InstantiateAsync<VehicleCardView>(asset, _root);
				cards.Add(card);
			}

			return cards;
		}
	}
}