using UnityEngine;

namespace CarSumo.Teams
{
    public class RandomTeamDefiner : ITeamDefiner
    {
        public Team DefineTeam(Team current)
        {
            return (Team) Random.Range(0, 2);
        }
    }
}