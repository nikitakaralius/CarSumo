using System.Collections.Generic;
using DataModel.Vehicles;
using Menu.Extensions;

namespace Menu.Cards
{
	public class CardDeck : ICardDeck
	{
		private readonly List<CardInDeck> _cards = new List<CardInDeck>();
		private readonly IPlacement _placement;
		private readonly ICardViewRepository _repository;

		public CardDeck(IPlacement placement, ICardViewRepository repository)
		{
			_placement = placement;
			_repository = repository;
		}

		public IEnumerable<CardInDeck> Cards => _cards;

		public void Draw(IVehicleDeck deck)
		{
			_cards.DestroyAndClear();
			for (int i = 0; i < deck.ActiveVehicles.Count; i++)
			{
				Vehicle vehicle = deck.ActiveVehicles[i];
				_cards.Add(_placement
					.AddFromPrefab(_repository.ViewOf(vehicle))
					.AddComponent<CardInDeck>()
					.Initialize(vehicle, i));
			}
		}
	}
}