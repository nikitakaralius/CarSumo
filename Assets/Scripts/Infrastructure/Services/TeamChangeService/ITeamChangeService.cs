using System;
using CarSumo.Teams;

namespace CarSumo.Infrastructure.Services.TeamChangeService
{
    public interface ITeamChangeService
    {
        Team ActiveTeam { get; }
        
        event Action TeamChanged;

        void ChangeOnNext();
    }
}