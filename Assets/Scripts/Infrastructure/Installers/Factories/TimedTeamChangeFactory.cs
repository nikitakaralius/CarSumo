using CarSumo.Teams;
using CarSumo.Teams.TeamChanging;
using Services.Timer;
using Zenject;

namespace Infrastructure.Installers.Factories
{
    public class TimedTeamChangeFactory : IFactory<TimedTeamChange>
    {
        private readonly ITeamDefiner _teamDefiner;
        private readonly ITimer _timer;
        private readonly IConfiguredTimerOperations _timerOperations;

        public TimedTeamChangeFactory(ITeamDefiner teamDefiner, ITimer timer, IConfiguredTimerOperations timerOperations)
        {
            _teamDefiner = teamDefiner;
            _timer = timer;
            _timerOperations = timerOperations;
        }

        public TimedTeamChange Create()
        {
            Team initialTeam = new RandomTeamDefiner().DefineNext(Team.First);
            return new TimedTeamChange(initialTeam, _teamDefiner, _timer, _timerOperations);
        }
    }
}