using UnityEngine;

namespace CarSumo.Audio.AudioData
{
    [CreateAssetMenu(menuName = "Audio/Audio Cue")]
    public class AudioCue : ScriptableObject
    {
        public AudioClip Clip => _audioClipsGroup.NextClip();

        [SerializeField] private AudioClipsGroup _audioClipsGroup;
    }
}