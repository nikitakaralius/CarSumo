using UnityEngine;

namespace CarSumo.Audio.AudioData
{
    public class SequentialAudioSequence : AudioSequence
    {
        public SequentialAudioSequence(AudioClip[] clips, ref int nextClipToPlay, ref int lastClipPlayed)
            : base(clips, ref nextClipToPlay, ref lastClipPlayed) { }

        public override AudioClip GetNextClip()
        {
            NextClipToPlay = (int) Mathf.Repeat(++NextClipToPlay, Clips.Length);
            LastClipPlayed = NextClipToPlay;
            return Clips[NextClipToPlay];
        }
    }
}