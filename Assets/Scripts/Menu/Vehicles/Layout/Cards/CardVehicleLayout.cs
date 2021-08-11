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
		[SerializeField] private float _holdTimeToDrag = 0.3f;

		private IAccountStorage _accountStorage;
		private VehicleCard _selectedCard;

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
			_selectedCard = null;
		}

		protected override void ProcessCreatedCollection(IEnumerable<VehicleCard> layout)
		{
			_selectedCard = null;
			_contentLayoutGroup.EnableElementsUpdate();

			foreach (VehicleCard vehicleCard in layout)
			{
				vehicleCard.SetSelectHandler(this);
				_vehicleScaling.ApplyInitialScale(vehicleCard.transform);
				
				vehicleCard.gameObject
					.AddComponent<VehicleCardDragHandler>()
					.Initialize(_holdTimeToDrag,
								() => NotifyOtherCards(Items, null),
								CollectionRoot, SelectedRoot,
								_contentLayoutGroup);
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

		public void AddVehicleToChange(VehicleId vehicle)
		{
			if (_selectedCard is null)
				return;

			_selectedCard.NotifyBeingDeselected();
			
			VehicleId[] newItems = Items
				.OrderBy(item => item.transform.GetSiblingIndex())
				.Select(item => item.VehicleId)
				.ToArray();
			
			newItems[_selectedCard.DynamicSiblingIndex] = vehicle;
			_accountStorage.ActiveAccount.Value.VehicleLayout.ChangeLayout(newItems);
		}

		private void NotifyOtherCards(IEnumerable<VehicleCard> allCards, VehicleCard selectedCard)
		{
			_selectedCard = selectedCard;
			
			IEnumerable<VehicleCard> otherCards = allCards.Where(card => card != selectedCard);

			foreach (VehicleCard card in otherCards)
			{
				card.NotifyBeingDeselected();
			}
		}
	}
}