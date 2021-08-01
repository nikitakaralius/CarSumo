using DataModel.FileData;
using Infrastructure.Installers.Bootstrap.SubContainers;
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
            BindFileService();
            BindProjectConfiguration();
            ProcessSubContainers();
        }

        private void ProcessSubContainers()
        {
            AccountsInstaller.Install(Container);
        }

        private void BindProjectConfiguration()
        {
            Container
                .BindInterfacesAndSelfTo<ProjectConfiguration>()
                .FromInstance(_configuration)
                .AsSingle()
                .NonLazy();
        }

        private void BindFileService()
        {
            Container
                .BindInterfacesAndSelfTo<JsonNetFileService>()
                .AsTransient()
                .NonLazy();
        }
    }
}