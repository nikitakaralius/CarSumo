using UnityEngine;
using CarSumo.VFX;

namespace CarSumo.Vehicles.Factory
{
    [CreateAssetMenu(fileName = "[Vehilce] {FX} factory", menuName = "CarSumo/Vehicles/FXFactory")]
    public class FXVehicleFactory : VehicleFactory
    {
        [SerializeField] private ParticlesFactory _instantiateParticles;

        public override Vehicle Create(Transform parent = null)
        {
            _instantiateParticles.Create(parent).Play();
            return base.Create(parent);
        }
    }
}
