using CarSumo.Teams;
using CarSumo.Teams.TeamChanging;
using Infrastructure.Installers.Factories;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class TeamsInstaller : Installer<TeamsInstaller>
    {
        public override void InstallBindings()
        {
            BindTeamDefiner();
            BindTeamChanging();
        }
        
        private void BindTeamDefiner()
        {
            Container
                .Bind<ITeamDefiner>()
                .To<SequentialTeamDefiner>()
                .AsSingle();
        }

        private void BindTeamChanging()
        {
            Container
                .BindInterfacesAndSelfTo<TimedTeamChange>()
                .FromFactory<TimedTeamChange, TimedTeamChangeFactory>()
                .AsSingle();
        }
    }
}