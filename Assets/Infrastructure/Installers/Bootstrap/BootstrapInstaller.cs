using Infrastructure.Installers.SubContainers;
using Infrastructure.Settings;
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
            ProcessSubContainers();
        }

        private void ProcessSubContainers()
        {
            AccountsInstaller.Install(Container);
            FileDataInstaller.Install(Container);
            SettingsInstaller.Install(Container);
            ResourcesInstaller.Install(Container);
            ServicesInstaller.Install(Container);
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