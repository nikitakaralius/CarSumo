using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using CarSumo.Extensions;
using CarSumo.Teams;
using CarSumo.Units;

namespace CarSumo.Storage
{
    public class UnitStorage : MonoBehaviour, IReactiveUnitStorage
    {
        public event Action<Unit> Added;
        public event Action<Unit> Removed;
        public event Action<Unit> Emptied;

        private List<Unit> _firstTeamUnitsAlive = new List<Unit>();
        private List<Unit> _secondTeamUnitsAlive = new List<Unit>();

        private void Awake()
        {
            _firstTeamUnitsAlive = FindUnitsByTeam(Team.First)
                .Every(unit => unit.Destroying += RemoveFromStorage)
                .ToList();

            _secondTeamUnitsAlive = FindUnitsByTeam(Team.Second)
                .Every(unit => unit.Destroying += RemoveFromStorage)
                .ToList();
        }

        public void Add(Unit element)
        {
            if (element.Team == Team.First)
            {
                _firstTeamUnitsAlive.Add(element);
            }
            else
            {
                _secondTeamUnitsAlive.Add(element);
            }

            element.Destroying += RemoveFromStorage;
            Added?.Invoke(element);
        }

        public void Remove(Unit element)
        {
            if (element.Team == Team.First)
            {
                _firstTeamUnitsAlive.Remove(element);
            }
            else
            {
                _secondTeamUnitsAlive.Remove(element);
            }

            Removed?.Invoke(element);

            if (_firstTeamUnitsAlive.Count == 0 || _secondTeamUnitsAlive.Count == 0)
                Emptied?.Invoke(element);
        }

        private IEnumerable<Unit> FindUnitsByTeam(Team team)
        {
            return FindObjectsOfType<Unit>().Where(unit => unit.Team == team);
        }

        private void RemoveFromStorage(Unit unit)
        {
            Remove(unit);
            unit.Destroying -= RemoveFromStorage;
        }
    }
}