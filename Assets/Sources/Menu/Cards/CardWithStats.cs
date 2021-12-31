using DataModel.Vehicles;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace Menu.Cards
{
	public class CardWithStats : SerializedMonoBehaviour, ICard
	{
		[SerializeField] private Vehicle _vehicle;
		[SerializeField] private TextMeshProUGUI _force;
		[SerializeField] private TextMeshProUGUI _speed;

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
			_speed.text = stats.Speed.ToString();
		}
	}
}