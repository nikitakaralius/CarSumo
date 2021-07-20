using CarSumo.Audio.Services;
using CarSumo.DataManagement.Core;
using CarSumo.GameSettings.Services;
using CarSumo.Infrastructure.Factories;
using CarSumo.Infrastructure.Services.LoadingScreen;
using CarSumo.Infrastructure.Services.SceneManagement;
using CarSumo.Infrastructure.StateMachine;
using DataManagement.Players.Services;
using DataManagement.Services;
using Zenject;

namespace CarSumo.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneLoadService();
            BindLoadingScreen();
            BindGameStateMachine();
            BindFileService();
            BindSettingsService();
            BindPlayersDataService();

            Container
                .Bind<IAudioPreferences>()
                .To<AudioPreferences>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayersDataService()
        {
            Container
                .Bind<PlayersDataService>()
                .FromFactory<PlayersDataServiceFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSettingsService()
        {
            Container
                .Bind<SettingsService>()
                .FromFactory<SettingsServiceFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void BindFileService()
        {
            Container
                .Bind<IFileService>()
                .To<JsonFileService>()
                .AsSingle();
        }

        private void BindLoadingScreen()
        {
            Container
                .Bind<ILoadingScreen>()
                .To<PrefabLoadingScreen>()
                .AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container
                .Bind<GameStateMachine>()
                .FromFactory<GameStateMachineRegistry>()
                .AsSingle()
                .NonLazy();
        }

        private void BindSceneLoadService()
        {
            Container
                .Bind<ISceneLoadService>()
                .FromInstance(new AddressablesSceneLoadService())
                .AsSingle()
                .NonLazy();
        }
    }

    public class AudioPreferencesFactory : IFactory<IAudioPreferences>
    {
        private readonly SettingsService _settingsService;

        public AudioPreferencesFactory(SettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public IAudioPreferences Create()
        {
            return new AudioPreferences(_settingsService);
        }
    }
}