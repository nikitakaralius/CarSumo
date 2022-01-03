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

		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();
		
		[Inject]
		private void Construct(IAccountStorage storage, ICardViewRepository repository)
		{
			_storage = storage;
			_deck = new CardDeck(_placement, repository);
		}
		
		public IEnumerable<CardInDeck> Cards => _deck.Cards;

		private IReadOnlyReactiveProperty<Account> ActiveAccount => _storage.ActiveAccount;

		private void Start()
		{
			ActiveAccount
				.Subscribe(x => _deck.Draw(x.VehicleDeck))
				.AddTo(_subscriptions);

			ActiveAccount.Value.VehicleDeck
				.ObserveLayoutCompletedChanging()
				.Subscribe(_deck.Draw)
				.AddTo(_subscriptions);
		}

		private void OnDestroy()
		{
			_subscriptions.Dispose();
		}
	}
}