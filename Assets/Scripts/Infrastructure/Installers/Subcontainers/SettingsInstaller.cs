using Infrastructure.Initialization;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class SettingsInstaller : Installer<SettingsInstaller>
    {
        public override void InstallBindings()
        {
            BindAudioSettingsInitialization();
        }

        private void BindAudioSettingsInitialization()
        {
            Container
                .Bind<AudioSettingsInitialization>()
                .FromNew()
                .AsSingle();
        }
    }
}