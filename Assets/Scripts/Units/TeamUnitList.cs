using CarSumo.Teams;
using System;
using System.Collections.Generic;

namespace CarSumo.Units
{
    public class TeamUnitList
    {
        private static int s_teamCount = Enum.GetValues(typeof(Team)).Length;

        private List<Unit>[] _units = new List<Unit>[s_teamCount];

        public TeamUnitList()
        {
            for (int i = 0; i < _units.Length; i++)
                _units[i] = new List<Unit>();
        }

        public void Add(Unit unit, Team team)
        {
            _units[(int)team].Add(unit);
        }

        public void Remove(Unit unit, Team team)
        {
            _units[(int)team].Remove(unit);
        }
    }
}
