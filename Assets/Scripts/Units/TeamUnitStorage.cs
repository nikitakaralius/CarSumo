using CarSumo.Teams;
using System.Collections.Generic;
using UnityEngine;

namespace CarSumo.Units
{
    public class TeamUnitStorage : MonoBehaviour, ITeamUnitStorage
    {
        private readonly Dictionary<Team, List<Unit>> _units = new Dictionary<Team, List<Unit>>
        {
            { Team.First, new List<Unit>() }, 
            { Team.Second, new List<Unit>() }
        };

        public void Add(Unit unit, Team team)
        {
            _units[team].Add(unit);
        }

        public void Remove(Unit unit, Team team)
        {
            _units[team].Remove(unit);
        }
    }
}
