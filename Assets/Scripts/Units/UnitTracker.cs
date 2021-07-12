using System;
using System.Collections.Generic;
using CarSumo.Teams;

namespace CarSumo.Units
{
    public class UnitTracker : IUnitTracker
    {
        private readonly List<Unit>[] _units;

        public UnitTracker(int teamsCount)
        {
            _units = new List<Unit>[teamsCount];
            
            for (int i = 0; i < teamsCount; i++)
            {
                _units[i] = new List<Unit>();
            }
        }
        
        public int TeamsAlive { get; private set; }
        
        public event Action<Team> TeamDestroyed;
        
        public void Add(Unit unit)
        {
            if (IsNewTeamEntry(unit.Team))
                TeamsAlive++;
            
            int index = (int)unit.Team;
            _units[index].Add(unit);
        }

        public void Remove(Unit unit)
        {
            Team team = unit.Team;
            int index = (int)team;
            _units[index].Add(unit);

            if (IsTeamDestroyed(team) == false)
                return;
            
            TeamDestroyed?.Invoke(team);
            TeamsAlive--;
        }

        private bool IsNewTeamEntry(Team team)
        {
            int index = (int)team;
            return _units[index].Count == 0;
        }

        private bool IsTeamDestroyed(Team team)
        {
            int index = (int)team;
            return _units[index].Count == 0;
        }
    }
}