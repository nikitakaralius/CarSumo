using System.Collections.Generic;
using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
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

		private CompositeDisposable _cardSubscriptions = new CompositeDisposable();
		
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
			_cardDeck.Cards.ForEach(x => x.StopPlayingReadyToChangeAnimation());
			_cardSubscriptions.Dispose();
			_view.Show();
			_selectedCard = card;
			transform.position = card.transform.position;
		}

		public void Change()
		{
			_cardSubscriptions = new CompositeDisposable();
			foreach (CardInDeck card in _cardDeck.Cards)
			{
				card.PlayReadyToChangeAnimation();
				card.ObserveOnClicked()
					.Subscribe(OnCardInDeckClicked)
					.AddTo(_cardSubscriptions);
			}
		}

		public void UpdatePosition()
		{
			if (_selectedCard is null)
			{
				return;
			}
			transform.position = _selectedCard.transform.position;
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
			_cardSubscriptions.Dispose();
			_view.Hide();
			_cardDeck.Cards.ForEach(x => x.StopPlayingReadyToChangeAnimation());
			_selectedCard = null;
		}
	}
}