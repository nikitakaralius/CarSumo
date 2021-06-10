using CarSumo.Abstract;
using CarSumo.Factory;
using UnityEngine;

namespace CarSumo.VFX
{
    [CreateAssetMenu(fileName = "Enablers Factory", menuName = "CarSumo/VFX/Enablers Factory")]
    public class EnablersFactory : FactoryScriptableObject<Enabler>
    {
        [SerializeField] private Enabler _enabler;

        public override Enabler Create(Transform parent = null)
        {
            return Instantiate(_enabler, parent);
        }
    }
}