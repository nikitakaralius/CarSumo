using CarSumo.VFX;
using UnityEngine;

namespace CarSumo.NewVehicles
{
    public class VehicleEngine : MonoBehaviour, IVehicleEngine
    {
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
            _exhaustParticles.Emit();
        }

        public void PushForward(Vector3 force)
        {
        }
    }
}
