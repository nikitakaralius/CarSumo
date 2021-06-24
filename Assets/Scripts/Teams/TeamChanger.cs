using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarSumo.Teams
{
    public class TeamChanger : SerializedMonoBehaviour, IReactiveTeamChangeHandler
    {
        public event Action<Team> TeamChanged;

        public Team Team { get; private set; }

        [SerializeField] private ITeamDefiner _onAwakeDefiner;
        [SerializeField] private ITeamDefiner _onGameDefiner;

        private void Awake()
        {
            var newTeam = _onAwakeDefiner.DefineTeam(Team);
            ChangeTeam(newTeam);
        }

        public void ChangeTeam()
        {
            ChangeTeam(_onGameDefiner.DefineTeam(Team));
        }

        private void ChangeTeam(Team team)
        {
            Team = team;
            TeamChanged?.Invoke(Team);
        }
    }
}