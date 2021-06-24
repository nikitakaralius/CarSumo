using System;

namespace CarSumo.Teams
{
    public interface ITeamChangeHandler
    {
        Team Team { get; }
        void ChangeTeam();
    }

    public interface IReactiveTeamChangeHandler : ITeamChangeHandler
    {
        event Action<Team> TeamChanged;
    }
}