using System;

namespace CarSumo.Teams
{
    public class SequentialTeamDefiner : ITeamDefiner
    {
        private static readonly int _teamsCount = Enum.GetNames(typeof(Team)).Length;

        public Team DefineTeam(Team current)
        {
            return (Team) ((int) (current + 1) % _teamsCount);
        }
    }
}