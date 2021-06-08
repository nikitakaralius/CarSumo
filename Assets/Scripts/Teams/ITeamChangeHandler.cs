using System;

namespace CarSumo.Teams
{
    public interface ITeamChangeHandler
    {
        Team Team { get; }
    }

    public interface IReactiveChangeHandler : ITeamChangeHandler
    {
        event Action<Team> TeamChanged;
    }
}