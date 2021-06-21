using CarSumo.VFX;
using UnityEngine;

namespace CarSumo.Vehicles.Factory
{
    [CreateAssetMenu(fileName = "VehicleFX Factory", menuName = "CarSumo/Vehicles/FactoryFX")]
    public class VehicleWithFXFactory : VehicleFactory
    {
        [SerializeField] private ParticlesFactory _particlesFactory;

        public override Vehicle Create(Transform parent = null)
        {
            var instance = base.Create(parent);
            _particlesFactory.Create(instance.transform).Play();   
            return instance;
        }
    }
}