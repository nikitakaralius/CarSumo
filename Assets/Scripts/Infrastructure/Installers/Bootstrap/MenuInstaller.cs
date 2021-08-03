using Infrastructure.Installers.SubContainers;
using Zenject;

namespace Infrastructure.Installers.Bootstrap
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            ProcessSubContainers();
        }

        private void ProcessSubContainers()
        {
            InstantiationInstaller.Install(Container);
        }
    }
}