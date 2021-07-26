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

        private IInstantiateService _instantiateService;

        [Inject]
        private void Construct(IInstantiateService instantiateService)
        {
            _instantiateService = instantiateService;
        }

        public virtual async Task<Vehicle> Create(Transform parent = null)
        {
            return await _instantiateService.InstantiateAsync<Vehicle>(_vehiclePrefab, parent);
        }
    }
}
