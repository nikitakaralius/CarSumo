namespace CarSumo.Teams
{
    public interface ITeamDefiner
    {
        Team DefineTeam(Team current);
    }

    public interface IPreviousTeamDefiner : ITeamDefiner
    {
        
    }
}