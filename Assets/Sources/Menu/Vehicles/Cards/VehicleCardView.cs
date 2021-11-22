using DataModel.Vehicles;
using Menu.Buttons;
using Sirenix.OdinInspector;
using Sources.Cards;
using TMPro;
using UnityEngine;
using Zenject;
using VehicleCard = Sources.Cards.VehicleCard;

namespace Sources.Menu.Vehicles.Cards
{
	public class VehicleCardView : SelectableButton<VehicleCardView>
	{
		[Header("Preferences")]
		[SerializeField] private VehicleId _vehicle;

		[Header("View Components")]
		[SerializeField, Required] private TextMeshProUGUI _power;
		[SerializeField, Required] private TextMeshProUGUI _fuel;

		[Inject] private readonly IVehicleCardsRepository _repository;

		public int CachedSiblingIndex { get; private set; }

		public VehicleId Vehicle => _vehicle;
		
		private void OnEnable()
		{
			VehicleCard vehicleCard = _repository.StatsOf(_vehicle);
			_power.text = vehicleCard.Power.ToString();
			_fuel.text = vehicleCard.Fuel.ToString();
		}
		
		public void Initialize(IButtonSelectHandler<VehicleCardView> selectHandler)
		{
			Initialize(this, selectHandler, false);
			UpdateSiblingIndex();
		}

		public void SetLatestSiblingIndex()
		{
			transform.SetSiblingIndex(CachedSiblingIndex);
		}

		public void UpdateSiblingIndex()
		{
			CachedSiblingIndex = transform.GetSiblingIndex();
		}

		protected override void OnButtonSelectedInternal()
		{
			UpdateSiblingIndex();
		}

		protected override void OnButtonDeselectedInternal() { }
	}
}