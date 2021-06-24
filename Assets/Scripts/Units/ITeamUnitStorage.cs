using CarSumo.Teams;

namespace CarSumo.Units
{
    public interface ITeamUnitStorage
    {
        void Add(Unit unit, Team team);
        void Remove(Unit unit, Team team);
    }
}
