using UnityEngine;

namespace AdvancedAudioSystem.Configuration
{
    public class VolumeConfiguration : IAudioConfiguration
    {
        [SerializeField, Range(0f, 1f)] private readonly float _volume = 1f;

        public void ApplyTo(AudioSource audioSource)
        {
            audioSource.volume = _volume;
        }
    }
}
