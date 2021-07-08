using UnityEngine;

namespace AdvancedAudioSystem.Emitters
{
    public class AudioSourceProperty : IAudioSourceProperty
    {
        private readonly AudioSource _source;

        public AudioSourceProperty(AudioSource source)
        {
            _source = source;
        }

        public bool IsPlaying => _source.isPlaying;

        public float Volume
        {
            get => _source.volume;
            set => _source.volume = value;
        }

        public float Pitch
        {
            get => _source.pitch;
            set => _source.pitch = value;
        }
    }
}
