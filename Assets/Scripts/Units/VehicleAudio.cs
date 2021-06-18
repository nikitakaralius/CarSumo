using CarSumo.Audio.AudioData;
using CarSumo.Audio.AudioData.Emitters;
using UnityEngine;

namespace CarSumo.Units
{
    public class VehicleAudio : MonoBehaviour
    {
        [SerializeField] private AudioConfigurationScriptableObject _configuration;
        [SerializeField] private MonoSoundEmitter _soundEmitter;
        
        [Header("Audio Cues")]
        [SerializeField] private AudioCue _collisionCue;

        public void PlayCollisionSound()
        {
            _soundEmitter.PlayAudioClip(_collisionCue.Clip, _configuration, false);
        }
    }
}