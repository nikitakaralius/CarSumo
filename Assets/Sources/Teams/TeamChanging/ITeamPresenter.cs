using UniRx;

namespace CarSumo.Teams.TeamChanging
{
    public interface ITeamPresenter
    {
        IReadOnlyReactiveProperty<Team> ActiveTeam { get; }
    }
}