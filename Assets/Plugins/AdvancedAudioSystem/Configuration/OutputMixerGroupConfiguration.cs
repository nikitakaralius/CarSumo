using UnityEngine;
using UnityEngine.Audio;

namespace AdvancedAudioSystem.Configuration
{
    public class OutputMixerGroupConfiguration : IAudioConfiguration
    {
        [SerializeField] private readonly AudioMixerGroup _outputAudioMixerGroup;

        public void ApplyTo(AudioSource audioSource)
        {
            audioSource.outputAudioMixerGroup = _outputAudioMixerGroup;
        }
    }
}
