using System.Threading.Tasks;
using CarSumo.Infrastructure.Services.Instantiate;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CarSumo.Vehicles.Factory
{
    [CreateAssetMenu(fileName = "{Vehilce} factory", menuName = "CarSumo/Vehicles/Factory")]
    public class VehicleFactory : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject _vehiclePrefab;

        private IAddressablesInstantiate _addressablesInstantiate;

        [Inject]
        private void Construct(IAddressablesInstantiate addressablesInstantiate)
        {
            _addressablesInstantiate = addressablesInstantiate;
        }

        public virtual async Task<Vehicle> Create(Transform parent = null)
        {
            return await _addressablesInstantiate.InstantiateAsync<Vehicle>(_vehiclePrefab, parent);
        }
    }
}
