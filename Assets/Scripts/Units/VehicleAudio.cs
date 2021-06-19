using UnityEngine;
using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;

namespace CarSumo.Units
{
    public class VehicleAudio : MonoBehaviour
    {
        [SerializeField] private MonoSoundEmitter _collisionSoundEmitter;
        [SerializeField] private MonoSoundEmitter _hornSoundEmitter;
        [SerializeField] private MonoSoundEmitter _engineSoundEmitter;
        
        [Header("Audio Cues")]
        [SerializeField] private AudioCue _collisionCue;
        [SerializeField] private AudioCue _hornCue;
        [SerializeField] private AudioCue _engineCue;

        [Header("Preferences")]
        [SerializeField, Range(0.0f, 1.0f)]
        private float _minVolume = 0.5f;

        [SerializeField, Range(0.0f, 1.0f)]
        private float _maxVolume = 1f;

        [SerializeField, Range(0.0f, 1.5f)]
        private float _minPitch = 0.7f;

        [SerializeField, Range(0.0f, 1.5f)]
        private float _maxPitch = 1.2f;

        private Vehicle _vehicle;

        private void Awake()
        {
            _vehicle = GetComponentInParent<Vehicle>();
        }

        private void OnEnable()
        {
            _vehicle.Picked += PlayEngineSound;
            _vehicle.StartingUp += ConfigureEngineByPowerPercentage;
            _vehicle.Pushed += PlayHornSound;
            _vehicle.Stopped += StopEngineSound;
            _vehicle.Unpicked += StopEngineSound;
        }

        private void OnDisable()
        {
            _vehicle.Picked -= PlayEngineSound;
            _vehicle.StartingUp -= ConfigureEngineByPowerPercentage;
            _vehicle.Pushed -= PlayHornSound;
            _vehicle.Stopped -= StopEngineSound;
            _vehicle.Unpicked -= StopEngineSound;
        }

        public void PlayCollisionSound()
        {
            _collisionSoundEmitter.Play(_collisionCue);
        }

        public void PlayHornSound()
        {
            _hornSoundEmitter.Play(_hornCue);
        }

        private void PlayEngineSound()
        {
            _engineSoundEmitter.Play(_engineCue);
        }

        private void ConfigureEngineByPowerPercentage(float percentage)
        {
            if (percentage < 0.0f || percentage > 100.0f)
                throw new System.ArgumentOutOfRangeException(nameof(percentage));

            _engineSoundEmitter.AudioSource.volume = Converter.MapByPercentsRange(percentage, _minVolume, _maxVolume);
            _engineSoundEmitter.AudioSource.pitch = Converter.MapByPercentsRange(percentage, _minPitch, _maxPitch);

            Debug.Log("Configuring");
        }

        private void StopEngineSound()
        {
            _engineSoundEmitter.Stop();
        }
    }
}