using UnityEngine;

namespace AdvancedAudioSystem.Emitters
{
    [RequireComponent(typeof(AudioSource))]
    public class UISoundEmitter : MonoBehaviour, ISoundEmitter
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        public void Play(AudioCue audioCue)
        {
            audioCue.PlayOn(_audioSource);
        }

        public void Stop()
        {
            _audioSource.Stop();
        }
    }
}