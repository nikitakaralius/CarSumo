using System.Collections.Generic;
using DataModel.Vehicles;
using Menu.Extensions;

namespace Menu.Deck
{
	public class MenuVehicleStorage
	{
		private readonly List<CardInStorage> _cards = new List<CardInStorage>();
		
		private readonly ICardRepository _repository;
		private readonly IVehicleStorage _storage;
		private readonly IPlacement _placement;

		public MenuVehicleStorage(ICardRepository repository, IVehicleStorage storage, IPlacement placement)
		{
			_repository = repository;
			_storage = storage;
			_placement = placement;
		}

		public IEnumerable<ICard> Cards => _cards;

		public void DrawCards(IVehicleDeck deck)
		{
			DrawCards(deck.ActiveVehicles);
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