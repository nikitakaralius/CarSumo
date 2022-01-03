using DataModel.Vehicles;
using UnityEngine.AddressableAssets;

namespace DataModel.GameData.Vehicles
{
    public interface IVehicleAssets
    {
        AssetReferenceGameObject GetAssetByVehicleId(Vehicle id);
    }
}