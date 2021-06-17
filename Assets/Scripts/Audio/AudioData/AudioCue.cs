using UnityEngine;

namespace CarSumo.Audio.AudioData
{
    [CreateAssetMenu(menuName = "Audio/Audio Cue")]
    public class AudioCue : ScriptableObject
    {
        [SerializeField] private bool _looping;
        [SerializeField] private AudioClipsGroup[] _audioClipsGroups;

        public AudioClip[] GetClips()
        {
            int clipsCount = _audioClipsGroups.Length;
            var resultingClips = new AudioClip[clipsCount];

            for (int i = 0; i < clipsCount; i++) 
                resultingClips[i] = _audioClipsGroups[i].NextClip();

            return resultingClips;
        }
    }
}