using CarSumo.DataManagement.Core;
using CarSumo.GameSettings.Management;
using CarSumo.Infrastructure.Factories;
using CarSumo.Infrastructure.Services.LoadingScreen;
using CarSumo.Infrastructure.Services.SceneManagement;
using CarSumo.Infrastructure.StateMachine;
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
        }

        private void BindSettingsService()
        {
            Container
                .Bind<SettingsService>()
                .FromFactory<SettingsServiceFactory>()
                .AsSingle();
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
}