using Menu.Vehicles.Cards;
using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using Menu.Extensions;
using Sirenix.Utilities;
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
		[SerializeField] private HorizontalLayoutGroup _contentLayoutGroup;

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
			_contentLayoutGroup.enabled = true;
			UpdateLayout(GetSortedLayoutVehicles());
			_selectedCard = null;
		}
		
		public void OnButtonSelected(VehicleCard element)
		{
			Transform cardTransform = element.transform;

			_contentLayoutGroup.enabled = false;

			_vehicleScaling.ApplySelectedAnimation(cardTransform);

			NotifyOtherCards(Items, element);

			Items.ForEach(item => item.UpdateSiblingIndex());
			cardTransform.SetParent(SelectedRoot);
		}

		public void OnButtonDeselected(VehicleCard element)
		{
			Transform cardTransform = element.transform;

			element.transform.SetParent(CollectionRoot);
			element.SetLatestSiblingIndex();
			_vehicleScaling.ApplyDeselectedAnimation(cardTransform);
		}

		protected override void ProcessCreatedCollection(IEnumerable<VehicleCard> layout)
		{
			_selectedCard = null;
			_contentLayoutGroup.enabled = true;

			foreach (VehicleCard vehicleCard in layout)
			{
				vehicleCard.Initialize(this);
				_vehicleScaling.ApplyInitialScale(vehicleCard.transform);

				vehicleCard.gameObject
					.AddComponent<VehicleCardDragHandler>()
					.Initialize(_holdTimeToDrag,
								() => NotifyOtherCards(Items, null),
								CollectionRoot,
								SelectedRoot,
								_contentLayoutGroup);
			}
		}

		public void AddVehicleToChange(VehicleId vehicle)
		{
			if (_selectedCard is null)
				return;

			_selectedCard.SetSelected(false);

			VehicleId[] newItems = GetSortedLayoutVehicles();
			newItems[_selectedCard.DynamicSiblingIndex] = vehicle;

			UpdateLayout(newItems);
		}

		private void NotifyOtherCards(IEnumerable<VehicleCard> allCards, VehicleCard selectedCard)
		{
			_selectedCard = selectedCard;

			IEnumerable<VehicleCard> otherCards = allCards.Where(card => card != selectedCard);

			foreach (VehicleCard card in otherCards)
			{
				card.SetSelected(false);
			}
		}

		private void UpdateLayout(IReadOnlyList<VehicleId> newLayout)
		{
			_accountStorage.ActiveAccount.Value.VehicleLayout.ChangeLayout(newLayout);
		}

		private VehicleId[] GetSortedLayoutVehicles()
		{
			VehicleId[] newItems = Items
				.OrderBy(item => item.transform.GetSiblingIndex())
				.Select(item => item.VehicleId)
				.ToArray();
			return newItems;
		}
	}
}