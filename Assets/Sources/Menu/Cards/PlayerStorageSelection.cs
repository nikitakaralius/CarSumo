using System.Collections.Generic;
using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Cards
{
	public class PlayerStorageSelection : SerializedMonoBehaviour, IStorageSelection
	{
		[SerializeField] private IPlacement _storagePlacement;
		[SerializeField] private ICardDeck _cardDeck;

		private IAccountStorage _accountStorage;
		private CardInStorage _selectedCard;

		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();
		
		[Inject]
		private void Construct(IAccountStorage accountStorage)
		{
			_accountStorage = accountStorage;
		}

		private IVehicleDeck Deck => _accountStorage.ActiveAccount.Value.VehicleDeck;

		private void OnDisable()
		{
			CompleteChanging();
		}

		public void Select(CardInStorage card)
		{
			_selectedCard = card;
		}

		public void Change()
		{
			_storagePlacement.Hide();
			
			foreach (CardInDeck card in _cardDeck.Cards)
			{
				card.PlayReadyToChangeAnimation();
				card.OnClicked()
					.Subscribe(OnCardInDeckClicked)
					.AddTo(_subscriptions);
			}
		}

		private void OnCardInDeckClicked(int position)
		{
			var deck = new List<Vehicle>(Deck.ActiveVehicles)
			{
				[position] = _selectedCard.Vehicle
			};
			Deck.ChangeLayout(deck);
			CompleteChanging();
		}

		private void CompleteChanging()
		{
			_storagePlacement.Show();
			_subscriptions.Dispose();
			foreach (CardInDeck card in _cardDeck.Cards)
			{
				card.StopPlayingReadyToChangeAnimation();
			}
		}
	}
}