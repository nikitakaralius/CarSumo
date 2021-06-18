using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;
using UnityEngine;

namespace CarSumo.Audio
{
    public class BackgroundMusic : MonoBehaviour
    {
        [SerializeField] private AudioCue _backgroundMusicCue;
        [SerializeField] private MonoSoundEmitter _soundEmitter;

        private void OnEnable()
        {
            _soundEmitter.FinishedPlaying += PlayNextClip;
        }

        private void OnDisable()
        {
            _soundEmitter.FinishedPlaying -= PlayNextClip;
        }

        private void Awake()
        {
            PlayNextClip();
        }

        private void PlayNextClip()
        {
            _soundEmitter.Play(_backgroundMusicCue);
        }
    }
}
