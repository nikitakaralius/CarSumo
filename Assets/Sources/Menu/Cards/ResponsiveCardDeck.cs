using System.Collections.Generic;
using CarSumo.DataModel.Accounts;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Cards
{
	public class ResponsiveCardDeck : SerializedMonoBehaviour, ICardDeck
	{
		[SerializeField] private IPlacement _placement;
		
		private CardDeck _deck;
		private IAccountStorage _storage;

		private CompositeDisposable _subscriptions;
		
		[Inject]
		private void Construct(IAccountStorage storage, ICardViewRepository repository)
		{
			_storage = storage;
			_deck = new CardDeck(_placement, repository);
		}
		
		public IEnumerable<CardInDeck> Cards => _deck.Cards;

		private IReadOnlyReactiveProperty<Account> ActiveAccount => _storage.ActiveAccount;

		private void OnEnable()
		{
			_subscriptions = new CompositeDisposable();
			ActiveAccount
				.Subscribe(account =>
				{
					_deck.Draw(account.VehicleDeck);
					account.VehicleDeck
						.ObserveLayoutCompletedChanging()
						.Subscribe(_deck.Draw)
						.AddTo(_subscriptions);
				})
				.AddTo(_subscriptions);
		}

		private void OnDisable()
		{
			_subscriptions.Dispose();
		}
	}
}