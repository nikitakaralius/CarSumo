using UnityEngine;

namespace CarSumo.Audio.AudioData
{
    [SequenceModeImplementation(SequenceMode.RandomNoImmediateRepeat)]
    public class RandomNoImmediateRepeatSequence : Sequence
    {
        public RandomNoImmediateRepeatSequence(AudioClip[] clips, SequenceData sequenceData)
            : base(clips, sequenceData) { }

        public override AudioClip GetNextClip()
        {
            do
            {
                SequenceData.NextClipToPlay = Random.Range(0, Clips.Length);
            } 
            while (SequenceData.NextClipToPlay == SequenceData.LastClipPlayed);

            SequenceData.LastClipPlayed = SequenceData.NextClipToPlay;

            return Clips[SequenceData.NextClipToPlay];
        }
    }
}