using Services.Timer;
using UniRx;

namespace CarSumo.Teams.TeamChanging
{
    public class TimedTeamChange : ITeamChange, ITeamPresenter
    {
        private readonly ITeamDefiner _teamDefiner;
        private readonly IConfiguredTimerOperations _timer;
        private readonly ReactiveProperty<Team> _activeTeam;

        public TimedTeamChange(ITeamDefiner teamDefiner, IConfiguredTimerOperations timer)
        {
            _teamDefiner = teamDefiner;
            _timer = timer;
        }

        public IReadOnlyReactiveProperty<Team> ActiveTeam => _activeTeam;

        public void Initialize(Team initialTeam)
        {
            _activeTeam.Value = initialTeam;
        }

        public void ChangeOnNextTeam()
        {
            Team nextTeam = _teamDefiner.DefineNext(_activeTeam.Value);
            ChangeOn(nextTeam);
        }

        private void ChangeOn(Team team)
        {
            _activeTeam.Value = team;
            _timer.Start();
        }
    }
}