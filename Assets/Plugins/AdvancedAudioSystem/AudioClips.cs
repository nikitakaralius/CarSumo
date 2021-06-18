using UnityEngine;
using System.Collections.Generic;

namespace AdvancedAudioSystem
{
    public class AudioClips : List<AudioClip>
    {
        public int LastClipPlayedIndex { get; private set; } = -1;

        public AudioClips() : this(0) { }

        public AudioClips(int capacity) : base(capacity) { }
    }

    public sealed class AudioClipsMember
    {
        public AudioClip Clip => _clips[Index];

        public int Index { get; }

        private readonly AudioClips _clips;

        public AudioClipsMember(AudioClips clips, int index)
        {
            _clips = clips;
            Index = index;
        }
    }
}