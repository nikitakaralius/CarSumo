using System;
using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Vehicles.Layout
{
    public abstract class VehicleLayoutView<T> : VehicleCollectionView<T> where T : Component
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
        private IReadOnlyReactiveProperty<IVehicleLayout> Layout => _accountStorage.ActiveAccount.Value.VehicleLayout;

        private void OnEnable()
        {
            _accountChangedSubscription = ActiveAccount.Subscribe(OnActiveAccountChanged);
        }

        private void OnDisable()
        {
            _layoutChangedSubscription?.Dispose();
            _accountChangedSubscription?.Dispose();
        }

        private void OnActiveAccountChanged(Account account)
        {
            _layoutChangedSubscription?.Dispose();
            _layoutChangedSubscription = Layout.Subscribe(async layout => await SpawnCollectionAsync(layout.ActiveVehicles));
        }
    }
}