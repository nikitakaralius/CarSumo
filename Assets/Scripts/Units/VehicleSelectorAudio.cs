using AdvancedAudioSystem;
using AdvancedAudioSystem.Emitters;
using UnityEngine;

namespace CarSumo.Units
{
    public class VehicleSelectorAudio : MonoBehaviour
    {
        [SerializeField] private MonoSoundEmitter _soundEmitter;
        [SerializeField] private AudioCue _engineCue;

        public void PlayEngineCueOnVehicle(Vehicle vehicle)
        {
            _soundEmitter.transform.position = vehicle.transform.position;
            _soundEmitter.AudioSource.volume = 0.0f;
            _soundEmitter.Play(_engineCue);
        }

        public void ConfigureVolumeByPercentage(float percentage)
        {
            _soundEmitter.AudioSource.volume = percentage / 100.0f;
        }

        public void StopEngineCue()
        {
            _soundEmitter.Stop();
        }
    }
}