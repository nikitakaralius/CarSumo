using DataModel.Vehicles;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Deck
{
	public class MenuVehicleStorageComponent : SerializedMonoBehaviour
	{
		[SerializeField] private IPlacement _placement;
		
		private IVehicleDeck _deck;
		private MenuVehicleStorage _storage;

		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();
		
		[Inject]
		private void Construct(ICardRepository repository, IVehicleStorage storage, IVehicleDeck deck)
		{
			_storage = new MenuVehicleStorage(repository, storage, _placement);
			_deck = deck;
		}

		private void OnEnable()
		{
			_deck
				.ObserveLayoutCompletedChanging()
				.Subscribe(_storage.DrawCards)
				.AddTo(_subscriptions);
		}

		private void OnDestroy()
		{
			_subscriptions.Dispose();
		}
	}
}