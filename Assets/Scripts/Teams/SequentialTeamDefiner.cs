using System;

namespace CarSumo.Teams
{
    public class SequentialTeamDefiner : ITeamDefiner
    {
        private int TeamCount => Enum.GetNames(typeof(Team)).Length;

        public Team DefineTeam(Team current)
        {
            return (Team) ((int) (current + 1) % TeamCount);
        }
    }
}