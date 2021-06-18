using UnityEngine;

namespace AdvancedAudioSystem.Configuration
{
    public interface IAudioConfiguration
    {
        void ApplyTo(AudioSource audioSource);
    }
}
