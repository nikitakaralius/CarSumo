using DataModel.DataPersistence;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class DataPersistenceInstaller : Installer<DataPersistenceInstaller>
    {
        public override void InstallBindings()
        {
            BindFileService();
        }

        private void BindFileService() =>
            Container
                .BindInterfacesAndSelfTo<JsonNetFileService>()
                .AsSingle()
                .NonLazy();
    }
}