using Menu.Vehicles.Cards;
using Menu.Vehicles.Storage;
using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using DataModel.Vehicles;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Vehicles.Layout
{
	public class CardVehicleVehicleLayoutView : VehicleLayoutView<VehicleCard>, IVehicleCardSelectHandler,
		IVehicleLayoutView
	{
		[Header("View Components")] [SerializeField]
		private Transform _layoutRoot;

		[SerializeField] private LayoutGroup _contentLayoutGroup;

		[Header("Layout Card Driven Components")]
		[SerializeField] private VehicleStorageView _storage;
		[SerializeField] private LayoutVehicleCardAnimation _animation;
		[SerializeField] private Vector3 _cardSize = Vector3.one * 0.75f;

		private IEnumerable<VehicleCard> _layout;
		private int _selectedSlot;

		private IAccountStorage _accountStorage;

		[Inject]
		private void Construct(IAccountStorage accountStorage)
		{
			_accountStorage = accountStorage;
		}

		protected override Transform CollectionRoot => _layoutRoot;
		private Transform CardSelectedRoot => transform;

		private IVehicleLayout Layout => _accountStorage.ActiveAccount.Value.VehicleLayout.Value;

		private void OnDisable()
		{
			_contentLayoutGroup.enabled = true;
			_storage.Disable();
			_selectedSlot = -1;
		}

		protected override void ProcessCreatedLayout(IEnumerable<VehicleCard> layout)
		{
			_layout = layout;

			foreach (VehicleCard card in _layout)
			{
				card.transform.localScale = _cardSize;
				card.SetSelectHandler(this);
			}
		}

		public void ChangeLayoutVehicle(VehicleId vehicleId)
		{
			_storage.Disable();
			_contentLayoutGroup.enabled = true;
			Layout.ChangeActiveVehicle(vehicleId, _selectedSlot);
		}

		public void OnCardSelected(VehicleCard card)
		{
			_contentLayoutGroup.enabled = false;

			_selectedSlot = card.DynamicSiblingIndex;
			card.transform.SetParent(CardSelectedRoot);

			_storage.Enable();
			_animation.ApplyIncreaseSizeAnimation(card.transform);

			NotifyOtherCards(card);
		}

		public void OnCardDeselected(VehicleCard card)
		{
			_storage.Disable();
			OnCardDeselectedInternal(card);
		}

		private void NotifyOtherCards(VehicleCard selectedCard)
		{
			IEnumerable<VehicleCard> otherCards = _layout.Where(card => card != selectedCard);

			foreach (VehicleCard card in otherCards)
			{
				card.NotifyBeingDeselected(true);
				OnCardDeselectedInternal(card);
			}
		}

		private void OnCardDeselectedInternal(VehicleCard card)
		{
			card.transform.SetParent(CollectionRoot);
			card.SetLatestSiblingIndex();
			_animation.ApplyDecreaseSizeAnimation(card.transform);
		}
	}
}