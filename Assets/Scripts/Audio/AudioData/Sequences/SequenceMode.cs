using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CarSumo.Audio.AudioData
{
    public static class AudioSequence
    {
        private static readonly Type _sequenceBaseType = typeof(Sequence);

        public static Sequence GetSequence(SequenceMode mode, AudioClip[] clips, SequenceData data)
        {
            return CreateInstance(GetAudioSequenceType(mode), clips, data);
        }

        private static Type GetAudioSequenceType(SequenceMode mode)
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => _sequenceBaseType.IsAssignableFrom(type))
                .FirstOrDefault(type =>
                    type.GetCustomAttribute<SequenceModeImplementationAttribute>().SequenceMode == mode);
        }

        private static Sequence CreateInstance(Type type, AudioClip[] clips, SequenceData data)
        {
            if (type == null)
                throw new NotImplementedException(nameof(type));

            var ctors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            object[] args = {clips, data};
            return (Sequence) ctors[0].Invoke(args);
        }
    }

    public enum SequenceMode
    {
        Random,
        RandomNoImmediateRepeat,
        Sequential
    }
}