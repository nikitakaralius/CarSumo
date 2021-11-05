using System;
using System.Collections.Generic;
using System.Linq;
using CarSumo.Teams;
using CarSumo.Vehicles;
using UniRx;

namespace CarSumo.Units.Tracking
{
	public class UnitTracker : IUnitTracker, IUnitTrackerOperations, IVehicleTracker
	{
		private readonly Dictionary<Team, ReactiveProperty<int>> _unitsCount;
		private readonly Dictionary<Team, List<IUnit>> _units;

		public UnitTracker()
		{
			_unitsCount = new Dictionary<Team, ReactiveProperty<int>>()
			{
				{Team.Blue, new ReactiveProperty<int>(0)},
				{Team.Red, new ReactiveProperty<int>(0)}
			};

			_units = new Dictionary<Team, List<IUnit>>()
			{
				{Team.Blue, new List<IUnit>()},
				{Team.Red, new List<IUnit>()}
			};
		}
		
		public IReadOnlyReactiveProperty<int> GetUnitsAlive(Team team)
		{
			return _unitsCount[team];
		}

		public void Add(IUnit unit)
		{
			Team key = unit.Team;
			
			if (_unitsCount.TryGetValue(key, out var count))
			{
				count.Value++;
			}
			else
			{
				_unitsCount[key] = new ReactiveProperty<int>(1);
			}
			
			_units[key].Add(unit);
		}

		public void Remove(IUnit unit)
		{
			Team key = unit.Team;

			if (_unitsCount.TryGetValue(key, out var count) == false)
			{
				throw new InvalidOperationException("Trying to remove unit with unregistered team");
			}

			if (count.Value <= 0)
			{
				throw new InvalidOperationException("Trying to remove unit in empty team");
			}

			count.Value--;
			_units[key].Remove(unit);
		}

		public IEnumerable<Vehicle> VehiclesBy(Team team)
		{
			return _units[team].Select(x => x.Vehicle);
		}
	}
}