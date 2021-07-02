using System;

namespace CarSumo.Teams
{
    public class PreviousSequentialTeamDefiner : ITeamDefiner
    {
        private int TeamCount => Enum.GetNames(typeof(Team)).Length;

        public Team DefineTeam(Team current)
        {
            return (Team)Math.Abs((int)(current - 1) % TeamCount);
        }
    }
}