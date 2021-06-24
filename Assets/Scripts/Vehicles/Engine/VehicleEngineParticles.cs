using CarSumo.VFX;
using UnityEngine;
using CarSumo.Vehicles.Speedometers;
using CarSumo.VFX.Core;
using System;

namespace CarSumo.Vehicles.Engine
{
    public class VehicleEngineParticles : MonoBehaviour
    {
        [SerializeField] private FXEmitter _exhaustParticles;
        [SerializeField] private FXEmitter _directionParticles;
        [SerializeField] private MonoEmitter _targetCircle;
        [SerializeField] private SpeedometerText _speedometerPowerText;

        private MonoEmitter[] _allParticles;

        private void OnEnable()
        {
            _allParticles = new MonoEmitter[]
            {
                _exhaustParticles,
                _directionParticles,
                _targetCircle,
                _speedometerPowerText
            };
        }

        public void EmitEngineTurnedOnParticles(IVehicleSpeedometer speedometer)
        {
            _exhaustParticles.Emit();
            _directionParticles.Emit();
            _targetCircle.Emit();
            _speedometerPowerText.Init(speedometer).Emit();
        }

        public void EmitExhaustParticlesUntil(Func<bool> predicate)
        {
            _exhaustParticles.EmitUntil(predicate);
        }

        public void StopAllParticles()
        {
            foreach (var emitter in _allParticles)
                emitter.Stop();
        }
    }
}
