using UnityEngine;

namespace AdvancedAudioSystem.Sequences
{
    public class SequentialSequence : IAudioSequence
    {
        public AudioClipsMember NextClipMember(AudioClips clips)
        {
            int index = (int)Mathf.Repeat(clips.LastClipPlayedIndex + 1, clips.Count);
            return new AudioClipsMember(clips, index);
        }
    }
}
