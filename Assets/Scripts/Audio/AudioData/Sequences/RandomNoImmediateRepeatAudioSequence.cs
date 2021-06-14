using UnityEngine;

namespace CarSumo.Audio.AudioData
{
    public class RandomNoImmediateRepeatAudioSequence : AudioSequence
    {
        public RandomNoImmediateRepeatAudioSequence(AudioClip[] clips, ref int nextClipToPlay, ref int lastClipPlayed)
            : base(clips, ref nextClipToPlay, ref lastClipPlayed) { }

        public override AudioClip GetNextClip()
        {
            do
            {
                NextClipToPlay = Random.Range(0, Clips.Length);
            } 
            while (NextClipToPlay == LastClipPlayed);

            LastClipPlayed = NextClipToPlay;
            return Clips[NextClipToPlay];
        }
    }
}