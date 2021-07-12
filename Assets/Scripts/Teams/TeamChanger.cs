using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CarSumo.Teams
{
    public class TeamChanger : SerializedMonoBehaviour, IReactiveTeamChangeHandler
    {
        public event Action<Team> TeamChanged;

        public Team Team { get; private set; }

        [SerializeField] private ITeamDefiner _onAwakeDefiner;
        
        private ITeamDefiner _onGameDefiner;

        [Inject]
        private void Construct(ITeamDefiner teamDefiner)
        {
            _onGameDefiner = teamDefiner;
        }

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