using System;
using CarSumo.Teams;

namespace CarSumo.Units
{
    public interface IUnitTracker
    {
        int TeamsAlive { get; }
        
        event Action<Team> TeamDestroyed;

        void Add(Unit unit);
        void Remove(Unit unit);
    }
}