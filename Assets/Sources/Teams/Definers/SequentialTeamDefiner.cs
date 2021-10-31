using UnityEngine;

namespace CarSumo.Teams
{
    public class SequentialTeamDefiner : AllTeamDefiner
    {
        public override Team DefineNext(Team current)
        {
            return (Team)((int)(current + 1) % TeamCount);
        }

        public override Team DefinePrevious(Team current)
        {
            var previousTeam = (Team) Mathf.Repeat((int)current - 1, TeamCount);

            return previousTeam;       
        }
    }
}