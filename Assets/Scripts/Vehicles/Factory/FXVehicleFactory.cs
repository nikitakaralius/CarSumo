using System.Threading.Tasks;
using AdvancedAudioSystem;
using CarSumo.VFX;
using UnityEngine;

namespace CarSumo.Vehicles.Factory
{
    [CreateAssetMenu(fileName = "[Vehilce] {FX} factory", menuName = "CarSumo/Vehicles/FXFactory")]
    public class FXVehicleFactory : VehicleFactory
    {
        [SerializeField] private ParticlesFactory _instantiateParticles;
        [SerializeField] private AudioCuePlayerScriptableObject _insantiateSound;

        public override async Task<Vehicle> Create(Transform parent = null)
        {
            var instance = await base.Create(parent);

            _instantiateParticles.Create(instance.transform).Play();
            _insantiateSound.PlayOn(instance.transform);

            return instance;
        }
    }
}
