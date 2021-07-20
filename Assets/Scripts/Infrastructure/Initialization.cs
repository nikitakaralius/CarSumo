using CarSumo.GameSettings.Services;
using CarSumo.Infrastructure.StateMachine;
using CarSumo.Infrastructure.StateMachine.States;
using DataManagement.Players.Models;
using DataManagement.Players.Services;
using UnityEngine;
using Zenject;

namespace CarSumo.Infrastructure
{
    public class Initialization : MonoBehaviour
    {
        private GameStateMachine _stateMachine;
        private SettingsService _settingsService;
        private PlayersDataService _playersDataService;

        [Inject]
        private void Construct(GameStateMachine stateMachine, SettingsService settingsService, PlayersDataService playersDataService)
        {
            _stateMachine = stateMachine;
            _settingsService = settingsService;
            _playersDataService = playersDataService;
        }

        private void Start()
        {
            _settingsService.Init();
            _playersDataService.Init();

            _stateMachine.Enter<BootstrapState>();
        }
    }
}