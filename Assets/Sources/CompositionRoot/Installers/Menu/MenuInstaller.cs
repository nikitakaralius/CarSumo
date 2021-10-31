using Infrastructure.Installers.SubContainers;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Menu
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