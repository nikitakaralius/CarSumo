using UnityEngine;

namespace AdvancedAudioSystem.Sequences
{
    public class RandomNoImmediateRepeatAudioSequence : IAudioSequence
    {
        public AudioClipsMember NextClipMember(AudioClips clips)
        {
            int index = 0;

            if (clips.Count == 1)
                return new AudioClipsMember(clips, index);

            do
            {
                index = Random.Range(0, clips.Count);
            } 
            while (index == clips.LastClipPlayedIndex);

            return new AudioClipsMember(clips, index);
        }
    }
}
