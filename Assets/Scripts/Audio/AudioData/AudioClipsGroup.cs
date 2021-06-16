using System;
using UnityEngine;

namespace CarSumo.Audio.AudioData
{
    [Serializable]
    public class AudioClipsGroup
    {
        public SequenceMode SequenceMode = SequenceMode.Sequential;
        public AudioClip[] AudioClips;

        private int _nextClipToPlay;
        private int _lastClipPlayed = -1;

        public AudioClip GetNextClip()
        {
            return AudioClips.Length == 1
                ? AudioClips[0]
                : GetSequence(SequenceMode).GetNextClip();
        }

        private AudioSequence GetSequence(SequenceMode mode)
        {
            return mode switch
            {
                SequenceMode.Sequential => new SequentialAudioSequence(AudioClips, ref _nextClipToPlay,
                    ref _lastClipPlayed),
                SequenceMode.Random => new RandomAudioSequence(AudioClips, ref _nextClipToPlay, ref _lastClipPlayed),
                SequenceMode.RandomNoImmediateRepeat
                    => new RandomNoImmediateRepeatAudioSequence(AudioClips, ref _nextClipToPlay, ref _lastClipPlayed),
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };
        }
    }

    public enum SequenceMode
    {
        Sequential,
        Random,
        RandomNoImmediateRepeat
    }
}