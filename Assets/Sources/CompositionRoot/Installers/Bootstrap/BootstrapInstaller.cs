using GameModes;
using Infrastructure.Installers.Factories;
using Infrastructure.Installers.SubContainers;
using Infrastructure.Settings;
using Menu.Resources;
using Services.SceneManagement;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Bootstrap
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private ProjectConfiguration _projectConfiguration;
        [SerializeField] private TimersConfiguration _timersConfiguration;
        
        public override void InstallBindings()
        {
            BindProjectConfiguration();
            BindTimersConfiguration();
            BindSceneLoading();
            BindGameModeRegistryInterfaces();
            ProcessSubContainers();
            BindProjectInitialization();
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

        private void BindProjectInitialization()
        {
            Container
                .Bind<ProjectInitialization>()
                .FromNew()
                .AsSingle();
        }

        private void BindSceneLoading()
        {
            Container
                .BindInterfacesAndSelfTo<AddressableSceneLoading>()
                .AsSingle()
                .NonLazy();
        }

        private void BindProjectConfiguration()
        {
            Container
                .BindInterfacesAndSelfTo<ProjectConfiguration>()
                .FromInstance(_projectConfiguration)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindTimersConfiguration()
        {
            Container
                .Bind<TimersConfiguration>()
                .FromInstance(_timersConfiguration)
                .AsSingle()
                .NonLazy();
        }

        private void BindGameModeRegistryInterfaces()
        {
	        Container
		        .BindInterfacesAndSelfTo<GameModeRegistry>()
		        .FromFactory<GameModeRegistry, GameModeRegistryFactory>()
		        .AsSingle();
        }
    }
}