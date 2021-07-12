using System;
using UnityEngine;

namespace CarSumo.Teams
{
    public class PreviousSequentialTeamDefiner : IPreviousTeamDefiner
    {
        private int TeamCount => Enum.GetNames(typeof(Team)).Length;

        public Team DefineTeam(Team current)
        {
            var previousTeam = (Team) Mathf.Repeat((int)current - 1, TeamCount);

            return previousTeam;
        }
    }
}