using AdvancedAudioSystem;
using Infrastructure.Installers.SubContainers;
using Menu.Accounts;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Menu
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private MonoAudioCuePlayer _audioCuePlayer;
        
        public override void InstallBindings()
        {
            BindAudioPlayer();
            ProcessSubContainers();
        }

        private void BindAudioPlayer()
        {
            Container
                .Bind<IAudioPlayer>()
                .FromInstance(_audioCuePlayer)
                .AsSingle();
        }

        private void ProcessSubContainers()
        {
            InstantiationInstaller.Install(Container);
        }
    }
}