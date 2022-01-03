using DataModel.Vehicles;
using Sirenix.OdinInspector;
using Sources.Core.Tests;
using TMPro;
using UnityEngine;
using Zenject;

namespace Menu.Cards
{
	public class CardWithStats : SerializedMonoBehaviour, ICard
	{
		[SerializeField] private Vehicle _vehicle;
		[SerializeField, ChildGameObjectsOnly, TestField("ForceText")] private TextMeshProUGUI _force;
		[SerializeField, ChildGameObjectsOnly, TestField("FuelText")] private TextMeshProUGUI _fuel;

		private ICardStatsRepository _repository;

		[Inject]
		private void Construct(ICardStatsRepository repository)
		{
			_repository = repository;
		}
		
		public Vehicle Vehicle => _vehicle;

		private void Start()
		{
			Bind(_repository.StatsOf(this));
		}

		private void Bind(VerboseVehicleStats stats)
		{
			_force.text = stats.Force.ToString();
			_fuel.text = stats.Fuel.ToString();
		}
	}
}