using System.Threading.Tasks;
using Services.Instantiate;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CarSumo.Vehicles.Factory
{
    [CreateAssetMenu(fileName = "{Vehilce} factory", menuName = "CarSumo/Vehicles/Factory")]
    public class VehicleFactory : ScriptableObject
    {
        [SerializeField] private AssetReferenceGameObject _vehiclePrefab;

        private IAsyncInstantiation _instantiation;

        [Inject]
        private void Construct(IAsyncInstantiation instantiation)
        {
            _instantiation = instantiation;
        }

        public virtual async Task<Vehicle> Create(Transform parent = null)
        {
            return await _instantiation.InstantiateAsync<Vehicle>(_vehiclePrefab, parent);
        }
    }
}
