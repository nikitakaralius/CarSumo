using System;
using System.Collections.Generic;
using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Vehicles.Layout
{
    public abstract class VehicleLayoutRenderer<T> : VehicleCollectionView<T> where T : Component
    {
        private IAccountStorage _accountStorage;

        private IDisposable _accountChangedSubscription;
        private IDisposable _layoutChangedSubscription;
        
        [Inject]
        private void Construct(IAccountStorage accountStorage)
        {
            _accountStorage = accountStorage;
        }
        
        private IReadOnlyReactiveProperty<Account> ActiveAccount => _accountStorage.ActiveAccount;
        private IEnumerable<VehicleId> Layout => _accountStorage.ActiveAccount.Value.VehicleLayout.ActiveVehicles;

        private void OnEnable()
        {
	        _accountChangedSubscription = ActiveAccount
                .Subscribe(OnActiveAccountChanged);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _accountChangedSubscription?.Dispose();
            _layoutChangedSubscription?.Dispose();
        }

        private async void OnActiveAccountChanged(Account account)
        {
            _layoutChangedSubscription?.Dispose();

            await SpawnCollectionAsync(Layout);
            
            _layoutChangedSubscription = account
	            .VehicleLayout
                .ObserveLayoutCompletedChanging()
	            .Subscribe(SpawnCollection);
        }

        private async void SpawnCollection(IEnumerable<VehicleId> _)
        {
            await SpawnCollectionAsync(Layout);
        }
    }
}