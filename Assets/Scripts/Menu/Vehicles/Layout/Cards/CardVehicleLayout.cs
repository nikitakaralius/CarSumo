using Menu.Vehicles.Cards;
using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using Menu.Extensions;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Vehicles.Layout
{
	public class CardVehicleLayout : VehicleLayoutRenderer<VehicleCard>, IVehicleCardSelectHandler
	{
		[Header("View Components")] 
		[SerializeField] private Transform _layoutRoot;
		[SerializeField] private LayoutGroup _contentLayoutGroup;

		[Header("Card Select Handle Components")] 
		[SerializeField] private CardVehicleLayoutScaling _vehicleScaling;

		private IAccountStorage _accountStorage;

		[Inject]
		private void Construct(IAccountStorage accountStorage)
		{
			_accountStorage = accountStorage;
		}

		protected override Transform CollectionRoot => _layoutRoot;

		private Transform SelectedRoot => transform;

		protected override void ProcessCreatedCollection(IEnumerable<VehicleCard> layout)
		{
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