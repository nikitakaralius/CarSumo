using DataModel.FileData;
using Infrastructure.Initialization;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class FileDataInstaller : Installer<FileDataInstaller>
    {
        public override void InstallBindings()
        {
            BindFileService();
            BindDataFilesInitialization();
        }

        private void BindDataFilesInitialization()
        {
            Container
                .Bind<DataFilesInitialization>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindFileService()
        {
            Container
                .BindInterfacesAndSelfTo<JsonNetFileService>()
                .AsSingle()
                .NonLazy();
        }
    }
}