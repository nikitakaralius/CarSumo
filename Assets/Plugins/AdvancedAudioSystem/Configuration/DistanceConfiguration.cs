using UnityEngine;

namespace AdvancedAudioSystem.Configuration
{
    public class DistanceConfiguration : IAudioConfiguration
    {
        [SerializeField, Range(0.01f, 10.0f)] private readonly float _minDistance = 0.1f;
        [SerializeField, Range(10.0f, 1000.0f)] private readonly float _maxDistance = 50.0f;

        public void ApplyTo(AudioSource audioSource)
        {
            audioSource.minDistance = _minDistance;
            audioSource.maxDistance = _maxDistance;
        }
    }
}
