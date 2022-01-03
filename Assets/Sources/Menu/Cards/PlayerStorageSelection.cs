using System.Collections.Generic;
using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using Sirenix.OdinInspector;
using Sources.Core.Interfaces;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Cards
{
	public class PlayerStorageSelection : SerializedMonoBehaviour, IStorageSelection
	{
		[SerializeField] private IPlacement _storagePlacement;
		[SerializeField] private ICardDeck _cardDeck;
		[SerializeField] private IVisible _view;

		private IAccountStorage _accountStorage;
		private CardInStorage _selectedCard;

		private CompositeDisposable _subscriptions;
		
		[Inject]
		private void Construct(IAccountStorage accountStorage)
		{
			_accountStorage = accountStorage;
		}

		private IVehicleDeck Deck => _accountStorage.ActiveAccount.Value.VehicleDeck;

		private void OnEnable()
		{
			_view.Hide();
		}
		
		private void OnDisable()
		{
			CompleteChanging();
		}

		public void Select(CardInStorage card)
		{
			_view.Show();
			_selectedCard = card;
			transform.position = card.transform.position;
		}

		public void Change()
		{
			_subscriptions = new CompositeDisposable();
			foreach (CardInDeck card in _cardDeck.Cards)
			{
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
			_view.Hide();
			foreach (CardInDeck card in _cardDeck.Cards)
			{
				card.StopPlayingReadyToChangeAnimation();
			}
		}
	}
}