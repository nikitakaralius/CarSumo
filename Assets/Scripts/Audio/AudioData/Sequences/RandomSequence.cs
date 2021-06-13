using UnityEngine;

namespace CarSumo.Audio.AudioData
{
    [SequenceModeImplementation(SequenceMode.Random)]
    public class RandomSequence : Sequence
    {
        public RandomSequence(AudioClip[] clips, SequenceData sequenceData) 
            : base(clips, sequenceData) {}

        public override AudioClip GetNextClip()
        {
            var nextClipToPlay = Random.Range(0, Clips.Length);
            SequenceData.LastClipPlayed = SequenceData.NextClipToPlay = nextClipToPlay;
            return Clips[nextClipToPlay];
        }
    }
}