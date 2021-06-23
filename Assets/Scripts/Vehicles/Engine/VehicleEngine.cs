﻿using UnityEngine;
using CarSumo.VFX;
using CarSumo.Vehicles.Speedometers;
using CarSumo.Vehicles.Stats;
using AdvancedAudioSystem;

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

        public void PushForward(float forceModifier)
        {
            var enginePower = _statsProvider.GetStats().EnginePower;
            var forceToAdd = transform.forward * forceModifier * enginePower;

            _rigidbody.AddForce(forceToAdd, ForceMode.Impulse);

            _engineSound.Stop();
            _engineSound.PlayUntil(() => _rigidbody.velocity.magnitude == 0.0f,
                              new MagnitudeSpeedometer(_rigidbody, _executor));

            _hornSound.Play();
        }
    }
}