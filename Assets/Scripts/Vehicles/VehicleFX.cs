using CarSumo.VFX;
using UnityEngine;

namespace CarSumo.Vehicles
{
    public class VehicleFX : MonoBehaviour
    {
        [SerializeField] private FXBehaviour _pushSmokeParticles;

        private Vehicle _vehicle;

        private void Awake()
        {
            _vehicle = GetComponentInParent<Vehicle>();
        }

        private void OnEnable()
        {
            _vehicle.Picked += EmitPushSmokeParticles;
            _vehicle.Unpicked += StopPushSmokeParticles;
            _vehicle.Stopped += StopPushSmokeParticles;
        }

        private void OnDisable()
        {
            _vehicle.Picked -= EmitPushSmokeParticles;
            _vehicle.Unpicked -= StopPushSmokeParticles;
            _vehicle.Stopped -= StopPushSmokeParticles;
        }

        private void EmitPushSmokeParticles() => _pushSmokeParticles.Emit();
        private void StopPushSmokeParticles() => _pushSmokeParticles.Stop();
    }
}