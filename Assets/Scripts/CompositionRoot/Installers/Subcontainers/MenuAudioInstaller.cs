using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.SubContainers
{
    public class MenuAudioInstaller : MonoInstaller
    {
        [SerializeField] private MonoAudioCuePlayer _audioCuePlayer;
        [SerializeField] private UISoundEmitter _soundEmitter;

        public override void InstallBindings()
        {
            BindAudioPlayer();
            BindSoundEmitter();
        }

        private void BindSoundEmitter()
        {
	        Container
		        .Bind<ISoundEmitter>()
		        .FromInstance(_soundEmitter)
		        .AsSingle();
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