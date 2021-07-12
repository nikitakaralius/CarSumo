using System;
using CarSumo.Teams;

namespace CarSumo.Infrastructure.Services.TeamChangeService
{
    public class TimedTeamChangeService : ITeamChangeService
    {
        private readonly ITeamDefiner _teamDefiner;

        public TimedTeamChangeService(ITeamDefiner teamDefiner)
        {
            _teamDefiner = teamDefiner;
            
            InitializeFirstTeam(new RandomTeamDefiner());
        }

        public Team ActiveTeam { get; private set; }
        
        public event Action TeamChanged;

        public void ChangeOnNext()
        {
            Team nextTeam = _teamDefiner.DefineNext(ActiveTeam);
            ChangeOn(nextTeam);
        }
        
        private void ChangeOn(Team team)
        {
            ActiveTeam = team;
            TeamChanged?.Invoke();
        }

        private void InitializeFirstTeam(ITeamDefiner teamDefiner)
        {
            Team initialTeam = teamDefiner.DefineNext(Team.First);
            ChangeOn(initialTeam);
        }
    }
}