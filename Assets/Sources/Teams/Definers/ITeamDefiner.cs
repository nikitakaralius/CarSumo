namespace CarSumo.Teams
{
    public interface ITeamDefiner
    {
        Team DefineNext(Team current);
        Team DefinePrevious(Team current);
    }
}