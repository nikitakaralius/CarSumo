﻿using System;
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
	        _accountChangedSubscription = ActiveAccount.Subscribe(OnActiveAccountChanged);
        }

        private void OnDisable()
        {
	        _layoutChangedSubscription?.Dispose();
            _accountChangedSubscription?.Dispose();
        }

        private async void OnActiveAccountChanged(Account account)
        {
            _layoutChangedSubscription?.Dispose();

            await SpawnCollectionAsync(Layout);
            
            _layoutChangedSubscription = account
	            .VehicleLayout.ObserveLayoutCompletedChanging()
	            .Subscribe(async _ => await SpawnCollectionAsync(Layout));
        }
    }
}