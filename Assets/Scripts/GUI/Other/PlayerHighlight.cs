using System.Collections.Generic;
using CarSumo.GUI.Core;
using CarSumo.Infrastructure.Services.TeamChangeService;
using CarSumo.Teams;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using GUIElement = CarSumo.GUI.Core.GUIElement;

namespace CarSumo.GUI.Other
{
    public class PlayerHighlight : SerializedMonoBehaviour
    {
        [SerializeField] private IReadOnlyDictionary<Team, GUIElement> _teamInfos;
        
        private ITeamChangeService _teamChangeService;
        private IGUIElement _activeElement = new EmptyGUIElement();

        [Inject]
        private void Construct(ITeamChangeService teamChangeService)
        {
            _teamChangeService = teamChangeService;
        }

        private void OnEnable()
        {
            _teamChangeService.TeamChanged += ChangeActiveTeamHighlight;
        }

        private void OnDisable()
        {
            _teamChangeService.TeamChanged -= ChangeActiveTeamHighlight;
        }

        private void Start()
        {
            ChangeActiveTeamHighlight();
        }

        private void ChangeActiveTeamHighlight()
        {
            _activeElement?.Stop();
            Team activeTeam = _teamChangeService.ActiveTeam;
            _activeElement = _teamInfos[activeTeam];
            _activeElement.Process();
        }
    }
}