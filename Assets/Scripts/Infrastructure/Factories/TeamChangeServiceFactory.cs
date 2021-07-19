using CarSumo.Infrastructure.Services.TeamChangeService;
using CarSumo.Infrastructure.Services.TimerService;
using CarSumo.Teams;
using Zenject;

namespace CarSumo.Infrastructure.Factories
{
    public class TeamChangeServiceFactory : IFactory<ITeamChangeService>
    {
        private readonly ITeamDefiner _teamDefiner;
        private readonly ITimerService _timerService;

        public TeamChangeServiceFactory(ITeamDefiner teamDefiner, ITimerService timerService)
        {
            _teamDefiner = teamDefiner;
            _timerService = timerService;
        }

        public ITeamChangeService Create()
        {
            return new TimedTeamChangeService(_teamDefiner, _timerService);
        }
    }
}