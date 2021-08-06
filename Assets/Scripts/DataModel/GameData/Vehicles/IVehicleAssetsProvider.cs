using DataModel.Vehicles;
using UnityEngine.AddressableAssets;

namespace DataModel.GameData.Vehicles
{
    public interface IVehicleAssetsProvider
    {
        AssetReferenceGameObject GetAssetByVehicleId(VehicleId id);
    }
}