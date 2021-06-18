using UnityEngine;

namespace AdvancedAudioSystem.Configuration
{
    public class RandomVolumeConfiguration : IAudioConfiguration
    {
        [SerializeField, Range(0f, 1f)] private readonly float _minVolume = 1f;
        [SerializeField, Range(0f, 1f)] private readonly float _maxVolume = 1f;

        public void ApplyTo(AudioSource audioSource)
        {
            audioSource.volume = Random.Range(_minVolume, _maxVolume);
        }
    }
}
