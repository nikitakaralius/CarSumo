using System.Threading.Tasks;
using CarSumo.StateMachine;
using CarSumo.StateMachine.States;
using Infrastructure.Initialization;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class ProjectInitialization : MonoBehaviour
    {
        private DataFilesInitialization _dataFilesInitialization;
        private ResourcesStorageInitialization _resourcesStorageInitialization;
        private AccountStorageInitialization _accountStorageInitialization;
        private AudioSettingsInitialization _audioSettingsInitialization;
        private GameStateMachine _stateMachine;

        [Inject]
        private void Construct(DataFilesInitialization dataFilesInitialization,
                               ResourcesStorageInitialization resourcesStorageInitialization,
                               AccountStorageInitialization accountStorageInitialization,
                               AudioSettingsInitialization audioSettingsInitialization,
                               GameStateMachine gameStateMachine)
        {
            _dataFilesInitialization = dataFilesInitialization;
            _resourcesStorageInitialization = resourcesStorageInitialization;
            _accountStorageInitialization = accountStorageInitialization;
            _audioSettingsInitialization = audioSettingsInitialization;
            _stateMachine = gameStateMachine;
        }
        
        private async void OnEnable()
        {
            _dataFilesInitialization.Initialize();

            await Task.WhenAll(
                _resourcesStorageInitialization.InitializeAsync(),
                _accountStorageInitialization.InitializeAsync(),
                _audioSettingsInitialization.InitializeAsync());
            
            _stateMachine.Enter<BootstrapState>();
        }
    }
}