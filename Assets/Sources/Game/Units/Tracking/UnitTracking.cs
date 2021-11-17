using System.Collections.Generic;
using CarSumo.Teams;
using UniRx;

namespace CarSumo.Units.Tracking
{
	public class UnitTracking : IUnitTracking, IUnitTrackingOperations
	{
		private readonly Dictionary<Team, ReactiveCollection<IUnit>> _units;

		public UnitTracking() =>
			_units = new Dictionary<Team, ReactiveCollection<IUnit>>
			{
				{Team.Blue, new ReactiveCollection<IUnit>()},
				{Team.Red, new ReactiveCollection<IUnit>()}
			};

		public IReadOnlyReactiveCollection<IUnit> UnitsAliveOf(Team team) => _units[team];

		public void Add(IUnit unit) => _units[unit.Team].Add(unit);

		public void Remove(IUnit unit) => _units[unit.Team].Remove(unit);
	}
}