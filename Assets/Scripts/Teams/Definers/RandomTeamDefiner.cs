using System;
using Random = UnityEngine.Random;

namespace CarSumo.Teams
{
    public class RandomTeamDefiner : AllTeamDefiner
    {
        public override Team DefineNext(Team current)
        {
            return (Team) Random.Range(0, TeamCount);
        }

        public override Team DefinePrevious(Team current)
        {
            return (Team) Random.Range(0, TeamCount);
        }
    }
}