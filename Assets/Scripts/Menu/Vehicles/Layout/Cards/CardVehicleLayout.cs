using Menu.Vehicles.Cards;
using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using Menu.Extensions;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Vehicles.Layout
{
	public class CardVehicleLayout : VehicleLayoutRenderer<VehicleCard>,
		IVehicleCardSelectHandler,
		IVehicleLayoutChanger
	{
		[Header("View Components")] 
		[SerializeField] private Transform _layoutRoot;
		[SerializeField] private LayoutGroup _contentLayoutGroup;

		[Header("Card Select Handle Components")] 
		[SerializeField] private CardVehicleLayoutScaling _vehicleScaling;

		private IAccountStorage _accountStorage;
		private int _selectedCardIndex = -1;

		[Inject]
		private void Construct(IAccountStorage accountStorage)
		{
			_accountStorage = accountStorage;
		}

		protected override Transform CollectionRoot => _layoutRoot;

		private Transform SelectedRoot => transform;

		private void OnDisable()
		{
			_contentLayoutGroup.EnableElementsUpdate();
			_selectedCardIndex = -1;
		}

		protected override void ProcessCreatedCollection(IEnumerable<VehicleCard> layout)
		{
			_selectedCardIndex = -1;
			_contentLayoutGroup.EnableElementsUpdate();

			foreach (VehicleCard vehicleCard in layout)
			{
				vehicleCard.SetSelectHandler(this);
				_vehicleScaling.ApplyInitialScale(vehicleCard.transform);
			}			
		}

		public void OnCardSelected(VehicleCard card)
		{
			Transform cardTransform = card.transform;

			_contentLayoutGroup.DisableElementsUpdate();

			_selectedCardIndex = card.DynamicSiblingIndex;
			cardTransform.SetParent(SelectedRoot);
			_vehicleScaling.ApplySelectedAnimation(cardTransform);

			NotifyOtherCards(Items, card);
		}

		public void OnCardDeselected(VehicleCard card)
		{
			Transform cardTransform = card.transform;

			card.transform.SetParent(CollectionRoot);
			card.SetLatestSiblingIndex();
			_vehicleScaling.ApplyDeselectedAnimation(cardTransform);
		}

		public bool TryChangeVehicleOn(VehicleId vehicle)
		{
			return _accountStorage.ActiveAccount.Value.VehicleLayout
				.TryChangeActiveVehicle(vehicle, _selectedCardIndex);
		}

		private void NotifyOtherCards(IEnumerable<VehicleCard> allCards, VehicleCard selectedCard)
		{
			IEnumerable<VehicleCard> otherCards = allCards.Where(card => card != selectedCard);

			foreach (VehicleCard card in otherCards)
			{
				card.NotifyBeingDeselected();
			}
		}
	}
}