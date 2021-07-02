using UnityEngine;
using Sirenix.OdinInspector;
using AdvancedAudioSystem.Configuration;

namespace AdvancedAudioSystem
{
    [CreateAssetMenu(fileName = "AudioCue", menuName = "Audio/AudioCue")]
    public class AudioCue : SerializedScriptableObject
    {
        [SerializeField] private AudioClipsGroup _audioClips;
        [SerializeField] private IAudioConfigurationEnumerable _configurations;
        public AudioClip Clip => _audioClips.NextClip();

        public void ApplyConfiguration(AudioSource source)
        {
            foreach (var configuration in _configurations)
            {
                configuration.ApplyTo(source);
            }
        }

        public void PlayOn(AudioSource source)
        {
            ApplyConfiguration(source);
            source.clip = Clip;
            source.time = 0.0f;
            source.Play();
        }
    }
}
