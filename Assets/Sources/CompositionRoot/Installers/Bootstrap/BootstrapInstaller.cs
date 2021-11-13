using DataModel.GameData.Infrastructure;
using GameModes;
using Infrastructure.Installers.Factories;
using Infrastructure.Installers.SubContainers;
using Services.SceneManagement;
using Zenject;

namespace Infrastructure.Installers.Bootstrap
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneLoading();
            BindGameModeRegistryInterfaces();
            ProcessSubContainers();
            BindProjectInitialization();
            BindApplicationEvents();
        }

        private void ProcessSubContainers()
        {
            FileDataInstaller.Install(Container);
            VehiclesInstaller.Install(Container);
            AccountsInstaller.Install(Container);
            ResourcesInstaller.Install(Container);
            StateMachineInstaller.Install(Container);
            AdvertisementInstaller.Install(Container);
        }

        private void BindProjectInitialization() =>
            Container
                .Bind<ProjectInitialization>()
                .FromNew()
                .AsSingle();

        private void BindSceneLoading() =>
            Container
                .BindInterfacesAndSelfTo<AddressableSceneLoading>()
                .AsSingle()
                .NonLazy();

        private void BindGameModeRegistryInterfaces() =>
            Container
                .BindInterfacesAndSelfTo<GameModeRegistry>()
                .FromFactory<GameModeRegistry, GameModeRegistryFactory>()
                .AsSingle();

        private void BindApplicationEvents() =>
            Container
                .BindInterfacesTo<ApplicationEvents>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();
    }
}