namespace CarSumo.Teams
{
    public class SequentialTeamDefiner : ITeamDefiner
    {
        private const int _teamsCount = 2;

        public Team DefineTeam(Team current)
        {
            return (Team) ((int) (current + 1) % _teamsCount);
        }
    }
}