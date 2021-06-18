using System;
using CarSumo.Sequences;
using UnityEngine;

namespace CarSumo.Audio.AudioData
{
    [Serializable]
    public class AudioClipsGroup
    {
        [SerializeField] private SequenceMode _sequenceMode;
        [SerializeField] private AudioClip[] _audioClips;

        private int _lastClipPlayed = -1;

        public AudioClip NextClip()
        {
            return _audioClips.Length == 1
                ? _audioClips[0]
                : NextClip(_sequenceMode);
        }

        private AudioClip NextClip(SequenceMode mode)
        {
            var sequence = CreateSequence(mode);
            var index = sequence.Next();
            _lastClipPlayed = index;
            return _audioClips[index];
        }

        private ISequence CreateSequence(SequenceMode mode)
        {
            int maxValue = _audioClips.Length;

            return mode switch
            {
                SequenceMode.Sequential => new SequentialSequence(_lastClipPlayed, maxValue),
                SequenceMode.Random => new RandomSequence(maxValue),
                SequenceMode.RandomNoImmediateRepeat => new RandomNoImmediateRepeatSequence(_lastClipPlayed, maxValue),
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