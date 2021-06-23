using UnityEngine;
using CarSumo.VFX;
using CarSumo.NewVehicles.Speedometers;

namespace CarSumo.NewVehicles
{
    public class VehicleEngine : MonoBehaviour, IVehicleEngine
    {
        [Header("Components")]
        [SerializeField] private FXEmitter _exhaustParticles;
        [SerializeField] private VehicleEngineSound _engineSound;

        private Rigidbody _rigidbody;
        private CoroutineExecutor _executor;

        public VehicleEngine Init(Rigidbody rigidbody, CoroutineExecutor executor)
        {
            _rigidbody = rigidbody;
            _executor = executor;

            return this;
        }

        public void TurnOn(IVehicleSpeedometer speedometer)
        {
            _engineSound.Play(() => false, speedometer);
            _exhaustParticles.Emit();
        }

        public void PushForward(Vector3 force)
        {
            _engineSound.Stop();
            _engineSound.Play(() => _rigidbody.velocity.magnitude == 0.0f,
                              new MagnitudeSpeedometer(_rigidbody, _executor));
        }
    }
}
