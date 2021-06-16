using UnityEngine;

namespace CarSumo.Audio.AudioData
{
    public class RandomAudioSequence : AudioSequence
    {
        public RandomAudioSequence(AudioClip[] clips, ref int nextClipToPlay, ref int lastClipPlayed)
            : base(clips, ref nextClipToPlay, ref lastClipPlayed) { }

        public override AudioClip GetNextClip()
        {
            NextClipToPlay = Random.Range(0, Clips.Length);
            LastClipPlayed = NextClipToPlay;
            return Clips[NextClipToPlay];
        }
    }
}