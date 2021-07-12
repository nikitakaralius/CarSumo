using CarSumo.Infrastructure.Services.TeamChangeService;
using CarSumo.Teams;
using Zenject;

namespace CarSumo.Infrastructure.Factories
{
    public class TeamChangeServiceFactory : IFactory<ITeamChangeService>
    {
        private readonly ITeamDefiner _teamDefiner;

        public TeamChangeServiceFactory(ITeamDefiner teamDefiner)
        {
            _teamDefiner = teamDefiner;
        }

        public ITeamChangeService Create()
        {
            return new TimedTeamChangeService(_teamDefiner);
        }
    }
}