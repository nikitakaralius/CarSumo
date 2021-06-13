using System;
using UnityEngine;

namespace CarSumo.Audio.AudioData
{
    [Serializable]
    public class AudioClipsGroup
    {
        public SequenceMode SequenceMode = SequenceMode.Sequential;
        public AudioClip[] AudioClips;

        private SequenceData _sequenceData = new SequenceData();

        public AudioClip GetNextClip()
        {
            return AudioClips.Length == 1
                ? AudioClips[0]
                : AudioSequence.GetSequence(SequenceMode, AudioClips, _sequenceData).GetNextClip();
        }
    }
}