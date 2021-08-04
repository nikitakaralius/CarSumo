using Infrastructure.Installers.SubContainers;
using Infrastructure.Settings;
using Services.SceneManagement;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Bootstrap
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private ProjectConfiguration _configuration;
        
        public override void InstallBindings()
        {
            BindProjectConfiguration();
            BindSceneLoading();
            ProcessSubContainers();
        }

        private void ProcessSubContainers()
        {
            FileDataInstaller.Install(Container);
            VehiclesInstaller.Install(Container);
            AccountsInstaller.Install(Container);
            SettingsInstaller.Install(Container);
            ResourcesInstaller.Install(Container);
            StateMachineInstaller.Install(Container);
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
                .FromInstance(_configuration)
                .AsSingle()
                .NonLazy();
        }
    }
}