using System;

namespace CarSumo.Audio.AudioData
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SequenceModeImplementationAttribute : Attribute
    {
        public SequenceMode SequenceMode { get; }

        public SequenceModeImplementationAttribute(SequenceMode sequenceMode)
        {
            SequenceMode = sequenceMode;
        }
    }
}