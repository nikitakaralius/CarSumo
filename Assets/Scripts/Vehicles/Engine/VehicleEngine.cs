using UnityEngine;
using CarSumo.VFX;
using CarSumo.Vehicles.Stats;
using AdvancedAudioSystem;
using CarSumo.Vehicles.Speedometers;
using System;

namespace CarSumo.Vehicles
{
    public class VehicleEngine : MonoBehaviour, IVehicleEngine
    {
        [Header("Components")]
        [SerializeField] private FXEmitter _exhaustParticles;
        [SerializeField] private VehicleEngineSound _engineSound;
        [SerializeField] private MonoAudioCuePlayer _hornSound;

        private Rigidbody _rigidbody;
        private CoroutineExecutor _executor;
        private IVehicleStatsProvider _statsProvider;

        public VehicleEngine Init(IVehicleStatsProvider statsProvider, Rigidbody rigidbody, CoroutineExecutor executor)
        {
            _rigidbody = rigidbody;
            _executor = executor;
            _statsProvider = statsProvider;

            return this;
        }

        public void TurnOn(IVehicleSpeedometer speedometer)
        {
            _engineSound.PlayUntil(() => false, speedometer);
            _exhaustParticles.Emit();
        }

        public void TurnOff()
        {
            _engineSound.Stop();
            _exhaustParticles.Stop();
        }

        public void SpeedUp(float forceModifier)
        {
            var enginePower = _statsProvider.GetStats().EnginePower;
            var forceToAdd = transform.forward * forceModifier * enginePower;

            _rigidbody.AddForce(forceToAdd, ForceMode.Impulse);

            Func<bool> cancel = () => _rigidbody.velocity.magnitude == 0.0f;

            _engineSound.Stop();
            _engineSound.PlayUntil(cancel,new MagnitudeSpeedometer(_rigidbody, _executor));

            _exhaustParticles.Stop();
            _exhaustParticles.EmitUntil(cancel);

            _hornSound.Play();
        }
    }
}
