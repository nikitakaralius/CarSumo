using CarSumo.Factory;
using UnityEngine;

namespace CarSumo.VFX
{
    [CreateAssetMenu(fileName = "Particles Factory", menuName = "CarSumo/VFX/Particles Factory")]
    public class ParticlesFactory : FactoryScriptableObject<ParticleSystem>
    {
        [SerializeField] private ParticleSystem _particles;

        public override ParticleSystem Create()
        {
            return Instantiate(_particles);
        }

        public ParticleSystem Create(Transform parent)
        {
            return Instantiate(_particles, parent);
        }
    }
}