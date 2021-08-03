using DataModel.FileData;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class FileDataInstaller : Installer<FileDataInstaller>
    {
        public override void InstallBindings()
        {
            BindFileService();
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