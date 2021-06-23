using CarSumo.Teams;

namespace CarSumo.Units
{
    public interface IUnitStorage
    {
        void Add(Unit unit, Team team);
        void Remove(Unit unit);
    }
}
