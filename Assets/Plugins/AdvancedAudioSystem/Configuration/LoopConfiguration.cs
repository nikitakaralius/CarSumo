using UnityEngine;

namespace AdvancedAudioSystem.Configuration
{
    public class LoopConfiguration : IAudioConfiguration
    {
        [SerializeField] private readonly bool _loop = true;

        public void ApplyTo(AudioSource audioSource)
        {
            audioSource.loop = _loop;
        }
    }
}
