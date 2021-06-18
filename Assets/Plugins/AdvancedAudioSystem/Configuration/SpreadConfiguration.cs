using UnityEngine;

namespace AdvancedAudioSystem.Configuration
{
    public class SpreadConfiguration : IAudioConfiguration
    {
        [SerializeField, Range(0f, 360f)] private readonly int _spread = 0;

        public void ApplyTo(AudioSource audioSource)
        {
            audioSource.spread = _spread;
        }
    }
}
