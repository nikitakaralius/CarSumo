using System.Collections.Generic;
using System.Linq;
using CarSumo.Audio.Services;
using CarSumo.GameSettings.Services;
using CarSumo.Infrastructure.StateMachine;
using CarSumo.Infrastructure.StateMachine.States;
using DataManagement.Players.Models;
using DataManagement.Players.Services;
using DataManagement.Resources.Models;
using UnityEngine;
using Zenject;

namespace CarSumo.Infrastructure
{
    public class Initialization : MonoBehaviour
    {
        private GameStateMachine _stateMachine;
        private SettingsService _settingsService;
        private PlayersDataService _playersDataService;

        private IAudioPreferences _audioPreferences;

        [Inject]
        private void Construct(IAudioPreferences audioPreferences, GameStateMachine stateMachine, SettingsService settingsService, PlayersDataService playersDataService)
        {
            _stateMachine = stateMachine;
            _settingsService = settingsService;
            _playersDataService = playersDataService;
            _audioPreferences = audioPreferences;

        }

        private void Start()
        {
            _settingsService.Init();
            _playersDataService.Init();

            _audioPreferences.Init();

            _stateMachine.Enter<BootstrapState>();
        }
    }
}