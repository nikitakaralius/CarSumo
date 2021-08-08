using System;
using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using Menu.Vehicles.Layout;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Vehicles.Storage
{
    public class VehicleStorageView : VehicleCollectionView<VehicleCard>
    {
        [SerializeField] private Transform _layoutRoot;

        private IVehicleStorage _vehicleStorage;
        private IAccountStorage _accountStorage;
        private IDisposable _storageChangedSubscription;

        [Inject]
        private void Construct(IVehicleStorage vehicleStorage, IAccountStorage accountStorage)
        {
            _vehicleStorage = vehicleStorage;
            _accountStorage = accountStorage;
        }

        protected override Transform CollectionRoot => _layoutRoot;

        private IReadOnlyReactiveCollection<VehicleId> BoughtVehicles => _vehicleStorage.BoughtVehicles;

        private IReadOnlyReactiveCollection<VehicleId> Layout =>
            _accountStorage.ActiveAccount.Value.VehicleLayout.Value.ActiveVehicles;

        private async void OnEnable()
        {
            await SpawnCollectionAsync(GetVehiclesExceptLayout());

            _storageChangedSubscription = BoughtVehicles
                .ObserveCountChanged()
                .Subscribe(async _ => await SpawnCollectionAsync(GetVehiclesExceptLayout()));
        }

        private void OnDisable()
        {
            _storageChangedSubscription.Dispose();
        }

        protected override void ProcessCreatedLayout(IEnumerable<VehicleCard> layout)
        {
        }

        private IEnumerable<VehicleId> GetVehiclesExceptLayout()
        {
            List<VehicleId> layout = new List<VehicleId>(Layout);

            foreach (VehicleId vehicle in BoughtVehicles)
            {
                if (layout.Any(layoutVehicle => layoutVehicle == vehicle))
                {
                    layout.Remove(vehicle);
                    continue;
                }

                yield return vehicle;
            }
        }
    }
}