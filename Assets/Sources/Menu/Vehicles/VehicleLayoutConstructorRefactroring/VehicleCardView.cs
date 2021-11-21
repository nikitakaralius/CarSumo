using DataModel.Vehicles;
using Sirenix.OdinInspector;
using Sources.Cards;
using TMPro;
using UnityEngine;
using Zenject;

namespace Menu.Vehicles.Refactoring
{
	public class VehicleCardView : MonoBehaviour
	{
		[Header("Preferences")]
		[SerializeField] private VehicleId _vehicle;


		[Header("View Components")]
		[SerializeField, Required] private TextMeshProUGUI _power;
		[SerializeField, Required] private TextMeshProUGUI _fuel;

		[Inject] private readonly IVehicleCardsRepository _repository;

		private void OnEnable()
		{
			VehicleCard vehicleCard = _repository.StatsOf(_vehicle);
			_power.text = vehicleCard.Power.ToString();
			_fuel.text = vehicleCard.Fuel.ToString();
		}
	}
}