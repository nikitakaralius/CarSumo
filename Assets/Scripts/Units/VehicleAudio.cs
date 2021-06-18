using UnityEngine;
using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;

namespace CarSumo.Units
{
    public class VehicleAudio : MonoBehaviour
    {
        [SerializeField] private MonoSoundEmitter _soundEmitter;
        [SerializeField] private Vehicle _vehicle;
        
        [Header("Audio Cues")]
        [SerializeField] private AudioCue _collisionCue;
        [SerializeField] private AudioCue _engineCue;

        private void OnEnable()
        {
            _vehicle.Pushed += PlayEngineSound;
        }

        private void OnDisable()
        {
            _vehicle.Pushed -= PlayEngineSound;
        }

        public void PlayCollisionSound()
        {
            _soundEmitter.Play(_collisionCue);
        }

        public void PlayEngineSound()
        {
            _soundEmitter.Play(_engineCue);
        }
    }
}