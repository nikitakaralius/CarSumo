using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using Menu.Vehicles.Cards;
using Menu.Vehicles.Layout;
using Sources.Menu.Vehicles.Cards;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Vehicles.Storage
{
    public class VehicleStorageView : VehicleCollectionView<VehicleCardView>, IVehicleCardSelectHandler
    {
	    [Header("View Components")]
        [SerializeField] private Transform _layoutRoot;
        
        [Header("Card Select Handle Components")]
        [SerializeField] private IVehicleLayoutChanger _layoutChanger;

        private IVehicleStorage _vehicleStorage;
        private IAccountStorage _accountStorage;
        private readonly CompositeDisposable _subscriptions = new CompositeDisposable();
        private IDisposable _layoutSubscription;

        [Inject]
        private void Construct(IVehicleStorage vehicleStorage, IAccountStorage accountStorage)
        {
            _vehicleStorage = vehicleStorage;
            _accountStorage = accountStorage;
        }

        protected override Transform CollectionRoot => _layoutRoot;

        private IReadOnlyReactiveCollection<VehicleId> BoughtVehicles => _vehicleStorage.BoughtVehicles;

        private IVehicleDeck Deck => _accountStorage.ActiveAccount.Value.VehicleDeck;

        private void OnEnable()
        {
            BoughtVehicles
                .ObserveCountChanged()
                .Subscribe(_ => SpawnPreparedCollectionAsync())
                .AddTo(_subscriptions);

            _accountStorage.ActiveAccount
	            .Subscribe(OnActiveAccountChanged)
	            .AddTo(_subscriptions);
        }

        private async void SpawnPreparedCollectionAsync()
        {
	        await SpawnPreparedCollectionAsync(Deck);
        }

        protected override void OnDisable()
        {
	        base.OnDisable();
	        
            _subscriptions.Dispose();
            _layoutSubscription?.Dispose();
        }
        
        public void OnButtonSelected(VehicleCardView element)
        {
	        _layoutChanger.AddVehicleToChange(element.Vehicle);
        }
        
        public void OnButtonDeselected(VehicleCardView element)
        {
	        
        }

        protected override void ProcessCreatedCollection(IEnumerable<VehicleCardView> layout)
        {
	        foreach (VehicleCardView card in layout)
	        {
		        card.Initialize(this);
	        }
        }

        private async Task SpawnPreparedCollectionAsync(IVehicleDeck deck)
        {
            IEnumerable<VehicleId> vehicles = GetVehiclesExceptLayout(deck);
            await SpawnCollectionAsync(vehicles);
        }

        private IEnumerable<VehicleId> GetVehiclesExceptLayout(IVehicleDeck deck)
        {
            List<VehicleId> cachedLayout = new List<VehicleId>(deck.ActiveVehicles);

            foreach (VehicleId vehicle in BoughtVehicles)
            {
                if (cachedLayout.Any(layoutVehicle => layoutVehicle == vehicle))
                {
                    cachedLayout.Remove(vehicle);
                    continue;
                }

                yield return vehicle;
            }
        }

        private async void OnActiveAccountChanged(Account account)
        {
	        _layoutSubscription?.Dispose();

	        await SpawnPreparedCollectionAsync(account.VehicleDeck);
	        
	        _layoutSubscription = Deck
		        .ObserveLayoutCompletedChanging()
		        .Subscribe(_ =>  SpawnPreparedCollectionAsync());
        }
    }
}