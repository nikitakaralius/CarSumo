using System;

namespace CarSumo.Teams
{
    public interface ITeamChangeHandler
    {
        Team Team { get; }
    }

    public interface IReactiveTeamChangeHandler : ITeamChangeHandler
    {
        event Action<Team> TeamChanged;
    }
}