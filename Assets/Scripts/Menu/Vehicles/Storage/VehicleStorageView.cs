using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using Menu.Vehicles.Cards;
using Menu.Vehicles.Layout;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Vehicles.Storage
{
    public class VehicleStorageView : VehicleCollectionView<VehicleCard>, IVehicleCardSelectHandler
    {
        [SerializeField] private Transform _layoutRoot;
        [SerializeField] private IVehicleLayoutView _layoutView;

        private IVehicleStorage _vehicleStorage;
        private IAccountStorage _accountStorage;
        private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

        [Inject]
        private void Construct(IVehicleStorage vehicleStorage, IAccountStorage accountStorage)
        {
            _vehicleStorage = vehicleStorage;
            _accountStorage = accountStorage;
        }

        protected override Transform CollectionRoot => _layoutRoot;

        private IReadOnlyReactiveCollection<VehicleId> BoughtVehicles => _vehicleStorage.BoughtVehicles;

        private IReactiveProperty<IVehicleLayout> Layout =>
            _accountStorage.ActiveAccount.Value.VehicleLayout;

        private void OnEnable()
        {
            BoughtVehicles
                .ObserveCountChanged()
                .Subscribe(async _ => await SpawnPreparedCollectionAsync(Layout.Value))
                .AddTo(_subscriptions);

            Layout
                .Subscribe(async layout => await SpawnPreparedCollectionAsync(layout))
                .AddTo(_subscriptions);
        }

        private void OnDisable()
        {
            _subscriptions.Dispose();
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void OnCardSelected(VehicleCard card)
        {
	        _layoutView.ChangeLayoutVehicle(card.VehicleId);
        }

        public void OnCardDeselected(VehicleCard card)
        {
        }

        protected override void ProcessCreatedLayout(IEnumerable<VehicleCard> layout)
        {
	        foreach (VehicleCard card in layout)
	        {
		        card.SetSelectHandler(this);
	        }
        }

        private async Task SpawnPreparedCollectionAsync(IVehicleLayout layout)
        {
            IEnumerable<VehicleId> vehicles = GetVehiclesExceptLayout(layout);
            await SpawnCollectionAsync(vehicles);
        }

        private IEnumerable<VehicleId> GetVehiclesExceptLayout(IVehicleLayout layout)
        {
            List<VehicleId> cachedLayout = new List<VehicleId>(layout.ActiveVehicles);

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
    }
}