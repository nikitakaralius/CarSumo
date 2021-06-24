using UnityEngine;
using CarSumo.Vehicles.Stats;
using AdvancedAudioSystem;
using CarSumo.Vehicles.Speedometers;
using System;

namespace CarSumo.Vehicles.Engine
{
    public class VehicleEngine : MonoBehaviour, IVehicleEngine
    {
        [Header("Components")]
        [SerializeField] private VehicleEngineParticles _particles;
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
            _particles.EmitEngineTurnedOnParticles(speedometer);
        }

        public void TurnOff()
        {
            _engineSound.Stop();
            _particles.StopAllParticles();
        }

        public void SpeedUp(float forceModifier)
        {
            var enginePower = _statsProvider.GetStats().EnginePower;
            var forceToAdd = transform.forward * forceModifier * enginePower;

            _rigidbody.AddForce(forceToAdd, ForceMode.Impulse);

            Func<bool> cancel = () => _rigidbody.velocity.magnitude == 0.0f;

            _engineSound.Stop();
            _engineSound.PlayUntil(cancel,new MagnitudeSpeedometer(_rigidbody, _executor));

            _particles.StopAllParticles();
            _particles.EmitExhaustParticlesUntil(cancel);

            _hornSound.Play();
        }
    }
}
