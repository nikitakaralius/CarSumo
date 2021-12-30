using System;
using System.Collections.Generic;
using System.Linq;
using DataModel.Vehicles;

namespace Menu.Deck
{
	public class MenuVehicleDeck
	{
		private readonly List<CardInDeck> _cards = new List<CardInDeck>();
		
		private readonly IPlacement _placement;
		private readonly IVehicleDeck _deck;
		private readonly ICardRepository _repository;

		public MenuVehicleDeck(IPlacement placement, IVehicleDeck deck, ICardRepository repository)
		{
			_placement = placement;
			_deck = deck;
			_repository = repository;
		}

		public IEnumerable<ICard> Cards => _cards;

		private IReadOnlyList<VehicleId> Ids => _cards.Select(card => card.VehicleId).ToArray();

		public void Initialize()
		{
			foreach (VehicleId vehicle in _deck.ActiveVehicles)
			{
				CardInDeck cardInDeck = CreateCard(vehicle);
				_cards.Add(cardInDeck);
			}
		}

		public void ReplaceWith(ICard card, int position)
		{
			if (position < 0 || position >= _cards.Count)
			{
				throw new InvalidOperationException(nameof(position));
			}
			CardInDeck cardInDeck = CreateCard(card);
			_cards[position] = cardInDeck;
			_deck.ChangeLayout(Ids);
		}

		private CardInDeck CreateCard(VehicleId vehicle)
		{
			return _placement
				.Add(_repository.ViewOf(vehicle))
				.AddComponent<CardInDeck>()
				.Initialize(vehicle);
		}

		private CardInDeck CreateCard(ICard card)
		{
			return CreateCard(card.VehicleId);
		}
	}
}
		
