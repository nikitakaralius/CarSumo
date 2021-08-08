using System;
using System.Collections.Generic;
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
        private IDisposable _storageChangedSubscription;
        
        [Inject]
        private void Construct(IVehicleStorage vehicleStorage)
        {
            _vehicleStorage = vehicleStorage;
        }
        
        protected override Transform CollectionRoot => _layoutRoot;

        private IReadOnlyReactiveCollection<VehicleId> BoughtVehicles => _vehicleStorage.BoughtVehicles;

        private void OnEnable()
        {
            _storageChangedSubscription = BoughtVehicles
                .ObserveCountChanged()
                .Subscribe(async _ => await SpawnCollectionAsync(BoughtVehicles));
        }

        private void OnDisable()
        {
            _storageChangedSubscription.Dispose();   
        }

        protected override void ProcessCreatedLayout(IEnumerable<VehicleCard> layout)
        {
        }
    }
}