using System;
using System.Collections.Generic;
using System.Linq;
using DataModel.Vehicles;
using Menu.Deck.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Menu.Deck
{
	public class MenuVehicleDeck : SerializedMonoBehaviour
	{
		[SerializeField] private IPlacement _placement; 
		
		private readonly List<CardInDeck> _cards = new List<CardInDeck>();

		private IVehicleDeckOperations _operations;
		private IVehicleDeck _deck;
		private ICardRepository _repository;

		[Inject]
		private void Construct(IVehicleDeck deck, IVehicleDeckOperations operations, ICardRepository repository)
		{
			_deck = deck;
			_operations = operations;
			_repository = repository;
		}
		
		private IReadOnlyList<VehicleId> Ids => _cards.Select(card => card.VehicleId).ToArray();

		private void Start()
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