using System.Collections.Generic;
using DataModel.Vehicles;
using Menu.Extensions;

namespace Menu.Cards
{
	public class CardStorage
	{
		private readonly List<CardInStorage> _cards = new List<CardInStorage>();
		private readonly IStorageSelection _selection;
		private readonly IPlacement _placement;
		private readonly ICardViewRepository _repository;

		public CardStorage(IStorageSelection selection, IPlacement placement, ICardViewRepository repository)
		{
			_selection = selection;
			_placement = placement;
			_repository = repository;
		}

		public IEnumerable<ICard> Cards => _cards;

		public void Draw(IVehicleStorage storage, IVehicleDeck deck)
		{
			_cards.DestroyAndClear();
			IEnumerable<Vehicle> vehiclesToDraw = storage.Without(deck);
			foreach (Vehicle vehicle in vehiclesToDraw)
			{
				_cards.Add(_placement
					.AddFromPrefab(_repository.ViewOf(vehicle))
					.AddComponent<CardInStorage>()
					.Initialize(vehicle, _selection));
			}
		}
	}
}