using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Vehicles.Storage
{
    public class VehicleStorageView : VehicleCollectionView<VehicleCard>
    {
        [SerializeField] private Transform _layoutRoot;
        [SerializeField] private LayoutGroup _layoutGroup;
        [SerializeField] private ScrollRect _scrollRect;

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

        private IVehicleLayout Layout =>
            _accountStorage.ActiveAccount.Value.VehicleLayout.Value;

        private void OnEnable()
        {
            BoughtVehicles
                .ObserveCountChanged()
                .Subscribe(async _ => await SpawnPreparedCollectionAsync(Layout))
                .AddTo(_subscriptions);

            _accountStorage.ActiveAccount
                .Subscribe(async account => await SpawnPreparedCollectionAsync(account.VehicleLayout.Value))
                .AddTo(_subscriptions);
        }

        private void OnDisable()
        {
            _subscriptions.Dispose();
        }

        protected override void ProcessCreatedLayout(IEnumerable<VehicleCard> layout)
        {
            foreach (VehicleCard card in layout)
            {
                card.Initialize(CollectionRoot, transform, _layoutGroup, _scrollRect);
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