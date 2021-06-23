using UnityEngine;
using CarSumo.VFX;
using AdvancedAudioSystem;

namespace CarSumo.Vehicles.Factory
{
    [CreateAssetMenu(fileName = "[Vehilce] {FX} factory", menuName = "CarSumo/Vehicles/FXFactory")]
    public class FXVehicleFactory : VehicleFactory
    {
        [SerializeField] private ParticlesFactory _instantiateParticles;
        [SerializeField] private AudioCuePlayerScriptableObject _insantiateSound;

        public override Vehicle Create(Transform parent = null)
        {
            _instantiateParticles.Create(parent).Play();
            _insantiateSound.PlayOn(parent);
            return base.Create(parent);
        }
    }
}
