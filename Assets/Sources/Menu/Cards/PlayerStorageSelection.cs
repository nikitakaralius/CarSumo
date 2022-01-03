using System;
using System.Collections.Generic;
using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Cards
{
	public class PlayerStorageSelection : SerializedMonoBehaviour, IStorageSelection
	{
		[SerializeField] private IPlacement _storagePlacement;
		[SerializeField] private ICardDeck _cardDeck;
		[SerializeField] private GameObject[] _viewElements = Array.Empty<GameObject>();

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
			HideView();
		}
		
		private void OnDisable()
		{
			CompleteChanging();
		}

		public void Select(CardInStorage card)
		{
			ShowView();
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
			HideView();
			foreach (CardInDeck card in _cardDeck.Cards)
			{
				card.StopPlayingReadyToChangeAnimation();
			}
		}

		private void HideView()
		{
			_viewElements.ForEach(x => x.SetActive(false));
		}
		
		private void ShowView()
		{
			_viewElements.ForEach(x => x.SetActive(true));
		}
	}
}