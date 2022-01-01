using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Cards
{
	public class ResponsiveCardStorage : SerializedMonoBehaviour
	{
		[SerializeField] private IPlacement _placement;
		[SerializeField] private IStorageSelection _selection;

		private IAccountStorage _accountStorage;
		private IVehicleStorage _vehicleStorage;
		
		private CardStorage _cardStorage;

		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();
		
		[Inject]
		private void Construct(IAccountStorage accountStorage, IVehicleStorage vehicleStorage, ICardViewRepository repository)
		{
			_accountStorage = accountStorage;
			_vehicleStorage = vehicleStorage;
			_cardStorage = new CardStorage(_selection, _placement, repository);
		}

		private IVehicleDeck Deck => _accountStorage.ActiveAccount.Value.VehicleDeck;

		private void OnEnable()
		{
			_accountStorage.ActiveAccount
				.Subscribe(x => _cardStorage.Draw(_vehicleStorage, Deck))
				.AddTo(_subscriptions);

			_vehicleStorage.BoughtVehicles
				.ObserveCountChanged()
				.Subscribe(x => _cardStorage.Draw(_vehicleStorage, Deck))
				.AddTo(_subscriptions);
		}

		private void OnDisable()
		{
			_subscriptions.Dispose();		
		}
	}
}