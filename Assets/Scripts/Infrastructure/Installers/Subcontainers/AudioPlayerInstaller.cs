using AdvancedAudioSystem;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class AudioPlayerInstaller : MonoInstaller
    {
        [SerializeField] private MonoAudioCuePlayer _audioCuePlayer;

        public override void InstallBindings()
        {
            BindAudioPlayer();
        }
        
        private void BindAudioPlayer()
        {
            Container
                .Bind<IAudioPlayer>()
                .FromInstance(_audioCuePlayer)
                .AsSingle();
        }
    }
}