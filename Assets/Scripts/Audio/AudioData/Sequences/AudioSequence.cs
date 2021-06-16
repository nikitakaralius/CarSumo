using System.Collections.Generic;
using UnityEngine;

namespace CarSumo.Audio.AudioData
{
    public abstract class AudioSequence
    {
        protected int NextClipToPlay;
        protected int LastClipPlayed;

        protected readonly AudioClip[] Clips;

        protected AudioSequence(AudioClip[] clips, ref int nextClipToPlay, ref int lastClipPlayed)
        {
            Clips = clips;
            NextClipToPlay = nextClipToPlay;
            LastClipPlayed = lastClipPlayed;
        }

        public abstract AudioClip GetNextClip();
    }
}