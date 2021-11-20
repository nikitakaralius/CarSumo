using System;
using System.Collections.Generic;
using System.Linq;
using DataModel.Vehicles;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DataModel.GameData.Vehicles
{
    [CreateAssetMenu(fileName = "VehicleAssetsProvider", menuName = "CarSumo/Vehicles/VehicleAssetsProvider")]
    public class VehicleAssetsSo : ScriptableObject, IVehicleAssets
    {
        //Odin has issues serializing AssetReference dictionary
        [Serializable]
        private struct VehicleAsset
        {
            public VehicleId Id;
            public AssetReferenceGameObject AssetReference;
        }

        [SerializeField] private VehicleAsset[] _vehicleAssets;

        private IReadOnlyDictionary<VehicleId, AssetReferenceGameObject> _assets;

        private void OnValidate()
        {
            if (_vehicleAssets.GroupBy(asset => asset.Id).Any(group => group.Count() > 1))
            {
                throw new InvalidOperationException("Assets should not contain duplicates");
            }

            _assets = _vehicleAssets.ToDictionary(x => x.Id, x => x.AssetReference);
        }

        private void OnEnable()
        {
            _assets = _vehicleAssets.ToDictionary(x => x.Id, x => x.AssetReference);
        }

        public AssetReferenceGameObject GetAssetByVehicleId(VehicleId id)
        {
            return _assets[id];
        }
    }
}