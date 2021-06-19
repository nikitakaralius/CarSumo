using UnityEngine;
using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;

namespace CarSumo.Units
{
    public class VehicleAudio : MonoBehaviour
    {
        [SerializeField] private MonoSoundEmitter _collisionSoundEmitter;
        [SerializeField] private MonoSoundEmitter _hornSoundEmitter;
        
        [Header("Audio Cues")]
        [SerializeField] private AudioCue _collisionCue;
        [SerializeField] private AudioCue _hornCue;

        private Vehicle _vehicle;

        private void Awake()
        {
            _vehicle = GetComponentInParent<Vehicle>();
        }

        private void OnEnable()
        {
            _vehicle.Pushed += PlayHornSound;
        }

        private void OnDisable()
        {
            _vehicle.Pushed -= PlayHornSound;
        }

        public void PlayCollisionSound()
        {
            _collisionSoundEmitter.Play(_collisionCue);
        }

        public void PlayHornSound()
        {
            _hornSoundEmitter.Play(_hornCue);
        }
    }
}