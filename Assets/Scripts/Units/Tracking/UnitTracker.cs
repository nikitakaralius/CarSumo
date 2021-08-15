using System;
using System.Collections.Generic;
using CarSumo.Teams;
using UniRx;

namespace CarSumo.Units.Tracking
{
	public class UnitTracker : IUnitTracker, IUnitTrackerOperations
	{
		private readonly Dictionary<Team, ReactiveProperty<int>> _unitsCount;

		public UnitTracker()
		{
			_unitsCount = new Dictionary<Team, ReactiveProperty<int>>()
			{
				{Team.Blue, new ReactiveProperty<int>(0)},
				{Team.Red, new ReactiveProperty<int>(0)}
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
		}
	}
}