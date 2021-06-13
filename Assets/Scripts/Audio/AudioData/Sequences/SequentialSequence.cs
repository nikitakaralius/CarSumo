using UnityEngine;

namespace CarSumo.Audio.AudioData
{
    [SequenceModeImplementation(SequenceMode.Sequential)]
    public class SequentialSequence : Sequence
    {
        public SequentialSequence(AudioClip[] clips, SequenceData sequenceData) : base(clips, sequenceData) { }

        public override AudioClip GetNextClip()
        {
            SequenceData.NextClipToPlay = (int) Mathf.Repeat(++SequenceData.NextClipToPlay, Clips.Length);
            SequenceData.LastClipPlayed = SequenceData.NextClipToPlay;
            return Clips[SequenceData.NextClipToPlay];
        }
    }
}