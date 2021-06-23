using CarSumo.Teams;
using UnityEngine;

namespace CarSumo.Units
{
    public class TeamUnitStorage : MonoBehaviour, ITeamUnitStorage
    {
        private readonly TeamUnitList _storage = new TeamUnitList();

        public void Add(Unit unit, Team team)
        {
            _storage.Add(unit, team);
        }

        public void Remove(Unit unit, Team team)
        {
            _storage.Remove(unit, team);
        }
    }
}
