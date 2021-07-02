using UnityEngine;
using Sirenix.OdinInspector;
using AdvancedAudioSystem.Sequences;

namespace AdvancedAudioSystem
{
    [CreateAssetMenu(fileName = "AudioClipsGroup", menuName = "Audio/AudioClipsGroup")]
    public class AudioClipsGroup : SerializedScriptableObject
    {
        [SerializeField] private AudioClips _audioClips = new AudioClips();
        [SerializeField] private IAudioSequence _sequenceMode = new SequentialSequence();

        public AudioClip NextClip()
        {
            return _audioClips.NextClipBySequence(_sequenceMode);
        }
    }
}
