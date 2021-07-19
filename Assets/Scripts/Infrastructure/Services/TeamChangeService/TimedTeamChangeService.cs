using System;
using CarSumo.Infrastructure.Services.TimerService;
using CarSumo.Teams;

namespace CarSumo.Infrastructure.Services.TeamChangeService
{
    public class TimedTeamChangeService : ITeamChangeService
    {
        private readonly ITeamDefiner _teamDefiner;
        private readonly ITimerService _timerService;

        public TimedTeamChangeService(ITeamDefiner teamDefiner, ITimerService timerService)
        {
            _teamDefiner = teamDefiner;
            _timerService = timerService;

            InitializeFirstTeam(new RandomTeamDefiner());

            _timerService.Elapsed += ChangeOnNext;
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
            _timerService.Start();
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