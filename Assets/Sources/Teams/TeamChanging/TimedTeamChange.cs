using Sources.Services.Timer.InGameTimer;
using UniRx;

namespace CarSumo.Teams.TeamChanging
{
    public class TimedTeamChange : ITeamChange, ITeamPresenter
    {
        private readonly ITeamDefiner _teamDefiner;
        private readonly ITimer _timer;
        private readonly IConfiguredTimerOperations _timerOperations;
        private readonly ReactiveProperty<Team> _activeTeam;

        public TimedTeamChange(Team initialTeam,
                               ITeamDefiner teamDefiner,
                               ITimer timer,
                               IConfiguredTimerOperations timerOperations)
        {
            _teamDefiner = teamDefiner;
            _timer = timer;
            _timerOperations = timerOperations;
            _activeTeam = new ReactiveProperty<Team>(initialTeam);
            _timer.Elapsed += ChangeOnNextTeam;
        }

        public IReadOnlyReactiveProperty<Team> ActiveTeam => _activeTeam;

        public void ChangeOnNextTeam()
        {
            Team nextTeam = _teamDefiner.DefineNext(_activeTeam.Value);
            ChangeOn(nextTeam);
        }

        private void ChangeOn(Team team)
        {
            _activeTeam.Value = team;
            _timerOperations.Start();
        }
    }
}