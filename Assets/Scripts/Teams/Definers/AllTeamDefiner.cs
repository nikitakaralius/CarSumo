using System;

namespace CarSumo.Teams
{
    public abstract class AllTeamDefiner : ITeamDefiner
    {
        protected int TeamCount => Enum.GetNames(typeof(Team)).Length;
        
        public abstract Team DefineNext(Team current);
        public abstract Team DefinePrevious(Team current);
    }
}