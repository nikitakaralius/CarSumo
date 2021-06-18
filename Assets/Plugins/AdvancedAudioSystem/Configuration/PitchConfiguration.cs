using UnityEngine;

namespace AdvancedAudioSystem.Configuration
{
    public class PitchConfiguration : IAudioConfiguration
    {
        [SerializeField, Range(-3f, 3f)] private readonly float _pitch = 1.0f;

        public void ApplyTo(AudioSource audioSource)
        {
            audioSource.pitch = _pitch;
        }
    }
}
