using CarSumo.Factory;
using UnityEngine;

namespace CarSumo.Vehicles.Factory
{
    [CreateAssetMenu(fileName = "[Vehilce] factory", menuName = "CarSumo/Vehicles/Factory")]
    public class VehicleFactory : FactoryScriptableObject<Vehicle>
    {
        [SerializeField] private Vehicle _vehiclePrefab;

        public override Vehicle Create(Transform parent = null)
        {
            return Instantiate(_vehiclePrefab, parent);
        }
    }
}
