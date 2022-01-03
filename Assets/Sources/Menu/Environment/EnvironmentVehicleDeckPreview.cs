using System.Collections.Generic;
using CarSumo.DataModel.Accounts;
using DataModel.GameData.Vehicles;
using DataModel.Vehicles;
using Menu.Extensions;
using Services.Instantiate;
using Sirenix.OdinInspector;
using Sources.Core.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Menu.Environment
{
	public class EnvironmentVehicleDeckPreview : SerializedMonoBehaviour
	{
		[SerializeField] private IVehicleAssets _assets;
		[SerializeField] private Transform _root;

		private IAccountStorage _accountStorage;
		private IAsyncInstantiation _instantiation;

		private int _index;

		private readonly List<Visible> _instances = new List<Visible>();
		private CompositeDisposable _subscriptions = new CompositeDisposable();

		[Inject]
		private void Construct(IAccountStorage accountStorage, IAsyncInstantiation instantiation)
		{
			_accountStorage = accountStorage;
			_instantiation = instantiation;
		}

		private IReadOnlyReactiveProperty<Account> ActiveAccount => _accountStorage.ActiveAccount;

		private void OnEnable()
		{
			_subscriptions = new CompositeDisposable();
			ActiveAccount
				.Subscribe(account =>
				{
					Create(account.VehicleDeck);
					account.VehicleDeck
						.ObserveLayoutCompletedChanging()
						.Subscribe(Create)
						.AddTo(_subscriptions);
				})
				.AddTo(_subscriptions);
		}

		private void OnDisable()
		{
			_subscriptions.Dispose();
		}

		public void Next()
		{
			Change(1);
		}

		public void Previous()
		{
			Change(-1);
		}

		private void Change(int iterator)
		{
			_instances[_index].Hide();
			_index = (int) Mathf.Repeat(_index + iterator, _instances.Count);
			_instances[_index].Show();
		}

		private async void Create(IVehicleDeck deck)
		{
			_instances.DestroyAndClear();
			_index = 0;
			foreach (Vehicle vehicle in deck.ActiveVehicles)
			{
				AssetReferenceGameObject asset = _assets.GetAssetByVehicleId(vehicle);
				var instance = await _instantiation.InstantiateAsync<Visible>(asset, _root);
				instance.Hide();
				_instances.Add(instance);
			}
			_instances[_index].Show();
		}
	}
}