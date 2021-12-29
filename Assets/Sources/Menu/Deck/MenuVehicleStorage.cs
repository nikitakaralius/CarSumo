using System;
using System.Collections.Generic;
using DataModel.Vehicles;
using Menu.Extensions;
using UniRx;
using Zenject;

namespace Menu.Deck
{
	public class MenuVehicleStorage : IInitializable, IDisposable
	{
		private readonly List<CardInStorage> _cards = new List<CardInStorage>();
		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();
		
		private readonly IVehicleDeck _deck;
		private readonly ICardRepository _repository;
		private readonly IVehicleStorage _storage;
		private readonly IPlacement _placement;

		public MenuVehicleStorage(IVehicleDeck deck, ICardRepository repository, IVehicleStorage storage, IPlacement placement)
		{
			_deck = deck;
			_repository = repository;
			_storage = storage;
			_placement = placement;
		}

		public IEnumerable<ICard> Cards => _cards;

		public void Initialize()
		{
			_deck
				.ObserveLayoutCompletedChanging()
				.Subscribe(DrawCards)
				.AddTo(_subscriptions);
		}

		public void Dispose()
		{
			_subscriptions.Dispose();
		}

		public void DrawCards(IEnumerable<VehicleId> deck)
		{
			_cards.DestroyAndClear();
			IEnumerable<VehicleId> storageToDraw = _storage.BoughtVehicles.Without(deck, (a, b) => a == b);
			foreach (VehicleId vehicle in storageToDraw)
			{
				_cards.Add(_placement
					.Add(_repository.ViewOf(vehicle))
					.AddComponent<CardInStorage>()
					.Initialize(vehicle));
			}
		}
	}
}