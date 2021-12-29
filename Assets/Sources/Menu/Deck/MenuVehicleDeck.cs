using System;
using System.Collections.Generic;
using System.Linq;
using DataModel.Vehicles;
using UnityEngine.AddressableAssets;

namespace Menu.Deck
{
	public class MenuVehicleDeck
	{
		private readonly List<CardInDeck> _cards = new List<CardInDeck>();
		
		private readonly IPlacement _placement;
		private readonly IVehicleDeckOperations _operations;
		private readonly IVehicleDeck _deck;
		private readonly ICardRepository _repository;

		public MenuVehicleDeck(IPlacement placement, IVehicleDeck deck, IVehicleDeckOperations operations, ICardRepository repository)
		{
			_placement = placement;
			_operations = operations;
			_deck = deck;
			_repository = repository;
		}

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
			_operations.ChangeLayout(Ids);
		}

		private CardInDeck CreateCard(VehicleId vehicle)
		{
			AssetReferenceGameObject view = _repository.ViewOf(vehicle);
			CardInDeck cardInDeck = _placement.Add(view).AddComponent<CardInDeck>();
			return cardInDeck;
		}

		private CardInDeck CreateCard(ICard card)
		{
			return CreateCard(card.VehicleId);
		}
	}
}
		
