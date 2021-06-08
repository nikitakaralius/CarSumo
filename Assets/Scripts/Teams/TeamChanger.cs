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
        [SerializeField] private ITeamChangeSender _changeSender;

        private void Awake()
        {
            var newTeam = _onAwakeDefiner.DefineTeam(Team);
            ChangeTeam(newTeam);
        }

        private void OnEnable() => _changeSender.ChangeSent += OnChangeTeamSent;

        private void OnDisable() => _changeSender.ChangeSent -= OnChangeTeamSent;

        private void OnChangeTeamSent()
        {
            var newTeam = _onGameDefiner.DefineTeam(Team);
            ChangeTeam(newTeam);
        }

        private void ChangeTeam(Team team)
        {
            Team = team;
            TeamChanged?.Invoke(Team);

            Debug.Log(Team);
        }
    }
}