using CarSumo.GameSettings.Management;
using CarSumo.Infrastructure.StateMachine;
using CarSumo.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace CarSumo.Infrastructure
{
    public class Initialization : MonoBehaviour
    {
        private GameStateMachine _stateMachine;
        private SettingsService _settingsService; 

        [Inject]
        private void Construct(GameStateMachine stateMachine, SettingsService settingsService)
        {
            _stateMachine = stateMachine;
            _settingsService = settingsService;
        }

        private void Start()
        {
            _settingsService.Init();
            
            _stateMachine.Enter<BootstrapState>();
        }
    }
}