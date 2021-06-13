using UnityEngine;

namespace CarSumo.Audio.AudioData
{
    public abstract class Sequence
    {
        protected readonly AudioClip[] Clips;
        protected readonly SequenceData SequenceData;

        public Sequence(AudioClip[] clips, SequenceData sequenceData)
        {
            Clips = clips;
            SequenceData = sequenceData;
        }

        public abstract AudioClip GetNextClip();
    }
}