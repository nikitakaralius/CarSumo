using UnityEngine;

namespace AdvancedAudioSystem.Sequences
{
    public class RandomAudioSequence : IAudioSequence
    {
        public AudioClipsMember NextClipMember(AudioClips clips)
        {
            int index = Random.Range(0, clips.Count);
            return new AudioClipsMember(clips, index);
        }
    }
}
