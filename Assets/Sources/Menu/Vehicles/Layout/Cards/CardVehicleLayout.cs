using Menu.Vehicles.Cards;
using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using Menu.Buttons;
using Menu.Extensions;
using Sirenix.Utilities;
using Sources.Menu.Vehicles.Cards;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Vehicles.Layout
{
	public class CardVehicleLayout : VehicleLayoutRenderer<VehicleCardView>,
		IButtonSelectHandler<VehicleCardView>,
		IVehicleLayoutChanger
	{
		[Header("View Components")] 
		[SerializeField] private Transform _layoutRoot;
		[SerializeField] private HorizontalLayoutGroup _contentLayoutGroup;

		[Header("Card Select Handle Components")] 
		[SerializeField] private CardVehicleLayoutScaling _vehicleScaling;
		[SerializeField] private float _holdTimeToDrag = 0.3f;

		private IAccountStorage _accountStorage;
		private VehicleCardView _selectedCard;

		[Inject]
		private void Construct(IAccountStorage accountStorage)
		{
			_accountStorage = accountStorage;
		}

		protected override Transform CollectionRoot => _layoutRoot;

		private Transform SelectedRoot => transform;

		protected override void OnDisable()
		{
			_contentLayoutGroup.EnableElementsUpdate();
			UpdateLayout(GetSortedLayoutVehicles());
			_selectedCard = null;
			
			base.OnDisable();
		}
		
		public void OnButtonSelected(VehicleCardView element)
		{
			Transform cardTransform = element.transform;

			_contentLayoutGroup.DisableElementsUpdate();

			_vehicleScaling.ApplySelectedAnimation(cardTransform);

			NotifyOtherCards(Items, element);

			Items.ForEach(item => item.UpdateSiblingIndex());
			cardTransform.SetParent(SelectedRoot);
		}

		public void OnButtonDeselected(VehicleCardView element)
		{
			Transform cardTransform = element.transform;

			element.transform.SetParent(CollectionRoot);
			element.SetLatestSiblingIndex();
			_vehicleScaling.ApplyDeselectedAnimation(cardTransform);
		}

		protected override void ProcessCreatedCollection(IEnumerable<VehicleCardView> layout)
		{
			_selectedCard = null;
			_contentLayoutGroup.EnableElementsUpdate();

			foreach (VehicleCardView card in layout)
			{
				card.Initialize(this);
				_vehicleScaling.ApplyInitialScale(card.transform);

				card.gameObject
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
			newItems[_selectedCard.CachedSiblingIndex] = vehicle;

			UpdateLayout(newItems);
		}

		private void NotifyOtherCards(IEnumerable<VehicleCardView> allCards, VehicleCardView selectedCard)
		{
			_selectedCard = selectedCard;

			IEnumerable<VehicleCardView> otherCards = allCards.Where(card => card != selectedCard);

			foreach (VehicleCardView card in otherCards)
			{
				card.SetSelected(false);
			}
		}

		private void UpdateLayout(IReadOnlyList<VehicleId> newLayout)
		{
			_accountStorage
				.ActiveAccount
				.Value
				.VehicleLayout
				.ChangeLayout(newLayout);
		}

		private VehicleId[] GetSortedLayoutVehicles() =>
			Items
				.OrderBy(item => item.transform.GetSiblingIndex())
				.Select(item => item.Vehicle)
				.ToArray();
	}
}