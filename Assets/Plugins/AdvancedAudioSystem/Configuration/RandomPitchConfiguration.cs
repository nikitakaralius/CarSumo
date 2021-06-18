using UnityEngine;

namespace AdvancedAudioSystem.Configuration
{
    public class RandomPitchConfiguration : IAudioConfiguration
    {
        [SerializeField] private readonly float _minPitch = 1.0f;
        [SerializeField] private readonly float _maxPitch = 1.0f;

        public void ApplyTo(AudioSource audioSource)
        {
            audioSource.pitch = Random.Range(_minPitch, _minPitch);
        }
    }
}
