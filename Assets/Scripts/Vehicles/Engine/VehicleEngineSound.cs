using UnityEngine;
using AdvancedAudioSystem.Emitters;
using AdvancedAudioSystem;
using System.Collections;
using System;
using .Assets.Scripts.Vehicles.Speedometers;

namespace CarSumo.Vehicles
{
    [RequireComponent(typeof(MonoSoundEmitter))]
    public class VehicleEngineSound : MonoBehaviour
    {
        [Header("Cue")]
        [SerializeField] private AudioCue _engineCue;

        [Header("Preferences")]
        [SerializeField] private Range _volumeRange;
        [SerializeField] private Range _pitchRange;

        private MonoSoundEmitter _emitter;

        private void Awake()
        {
            _emitter = GetComponent<MonoSoundEmitter>();
        }

        public void PlayUntil(Func<bool> cancel, IVehicleSpeedometer speedometer)
        {
            StartCoroutine(PlayUntilRoutine(cancel, speedometer));
        }

        public void Stop() => _emitter.Stop();

        public void ConfigureEngineSound(float enginePowerPercentage)
        {
            _emitter.AudioSource.volume = Converter.MapByPercentsRange(enginePowerPercentage, _volumeRange);
            _emitter.AudioSource.pitch = Converter.MapByPercentsRange(enginePowerPercentage, _pitchRange);
        }

        private IEnumerator PlayUntilRoutine(Func<bool> cancel, IVehicleSpeedometer speedometer)
        {
            _emitter.Play(_engineCue);

            while (cancel.Invoke() == false)
            {
                ConfigureEngineSound(speedometer.PowerPercentage);
                yield return null;
            }

            _emitter.Stop();
        }
    }
}
