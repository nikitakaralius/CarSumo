using UnityEngine;

namespace AdvancedAudioSystem.Configuration
{
    public class RolloffConfiguration : IAudioConfiguration
    {
        [SerializeField] private AudioRolloffMode _rolloffMode = AudioRolloffMode.Logarithmic;

        public void ApplyTo(AudioSource audioSource)
        {
            audioSource.rolloffMode = _rolloffMode;
        }
    }
}
